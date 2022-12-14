/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviourTree.Nodes;
using AI.BehaviourTree.Nodes.Composites;
using AI.BehaviourTree.Nodes.Decorators;
using Node = AI.BehaviourTree.Nodes.Node;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI.BehaviourTree
{
    public enum TreeOrientation
    {
        Vertical,
        Horizontal
    }

    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        [SerializeField, HideInInspector]
        private EcsEntity _agent;

        [NonSerialized] public State TreeState;
        [SerializeField, HideInInspector] public Node RootNode;
        [SerializeField, HideInInspector] public List<Node> Nodes = new List<Node>();
        [SerializeField, HideInInspector] public List<GroupSO> Groups = new List<GroupSO>();
        [SerializeField, HideInInspector] public TreeOrientation OrientationTree = TreeOrientation.Horizontal;
                
        [NonSerialized] private Node _prevNode;
        [NonSerialized] private Node _currentNode;

        public Action BehaviourTreeChanged; 
        public EcsEntity Agent => _agent;        

        private void OnDestroy() { BehaviourTreeChanged -= OnBehaviourTreeChanged; }
        private void OnBehaviourTreeChanged() { SetCurrentNode(RootNode); }
        private BehaviorTree CloneNodes()
        {
            BehaviorTree clone = Instantiate(this);
            clone.Nodes = Nodes.ConvertAll(child => child.Clone());
            clone.RootNode = clone.Nodes.First(i => i is RootNode);
            return clone;
        }
        
        public void Init(ref EcsEntity agent)
        {
            _currentNode = RootNode;
            _agent = agent;
            BehaviourTreeChanged += OnBehaviourTreeChanged;
            
            foreach (var node in Nodes) node.Init(this);
        }
        public State Update()
        {
            //проход по всем узлам - параметрам
            IEnumerable<ParameterNode> parameterNodes = Nodes.OfType<ParameterNode>();
            foreach (var node in parameterNodes)
            {
                node.Update();
            }

            //проход по всем узлам - условиям
            IEnumerable<ConditionNode> conditionNodes = Nodes.OfType<ConditionNode>();
            foreach (var node in conditionNodes)
            {
                node.Update();
            }

            //проверки текущего состояния
            if (RootNode.State == State.Success || RootNode.State == State.Failure)
                return RootNode.State;

            // TODO: replace by GetState(_prevNode)
            //============================
            if (_currentNode == null && _prevNode.State == State.Success)
                return State.Success;
            else if (_currentNode == null)
                return State.Failure;

            //обновление текущего узла
            if (_currentNode.State == State.Running)
                return _currentNode.Update();
            else
                SetCurrentNode(_currentNode.Parent);

            return State.Running;
            //============================
        }
        public BehaviorTree Clone()
        {
            BehaviorTree clone = CloneNodes();
            CloneEdges(this, clone);
            return clone;
        }
        
        // TODO something with this...
        public void CloneEdges(BehaviorTree source, BehaviorTree destination)
        {
            foreach (var node in destination.Nodes)
            {
                var originalNode = source.Nodes.Find(i => i.Equals(node));
                switch (originalNode)
                {
                    case ActionNode:
                        var cloneActionNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode)) as ActionNode;
                        if (((ActionNode) originalNode).Parent != null)
                        {
                            var cloneParentActionNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode.Parent));
                            cloneActionNode.Parent = cloneParentActionNode;
                        }
                        else 
                            cloneActionNode.Parent = null;
                        break;
                    case CompositeNode:
                        ChoiceNode choiceNode = originalNode as ChoiceNode;
                        if (choiceNode)
                        {
                            var cloneChoiceNode = destination.Nodes.FirstOrDefault(i => i.Equals(choiceNode)) as ChoiceNode;
                            foreach (var parameterNode in choiceNode.ParametersList)
                            {
                                var cloneParameter = destination.Nodes.FirstOrDefault(i => i.Equals(parameterNode)) as ParameterNode;
                                cloneParameter.ChildNode = cloneChoiceNode;
                                cloneChoiceNode.ParametersList.Add(cloneParameter);
                            }

                            foreach (var child in choiceNode.ChildNodes)
                            {
                                var cloneChild = destination.Nodes.FirstOrDefault(i => i.Equals(child));
                                cloneChoiceNode.ChildNodes.Add(cloneChild);
                            }

                            if (choiceNode.Parent != null)
                            {
                                var cloneParent = destination.Nodes.FirstOrDefault(i => i.Equals(choiceNode.Parent));
                                cloneChoiceNode.Parent = cloneParent;
                            }
                            else 
                                cloneChoiceNode.Parent = null;
                            
                            break;
                        }
                        SequencerNode sequencerNode = originalNode as SequencerNode;
                        if (sequencerNode)
                        {
                            var cloneSequencerNode = destination.Nodes.FirstOrDefault(i => i.Equals(sequencerNode)) as SequencerNode;
                            foreach (var child in sequencerNode.ChildNodes)
                            {
                                var cloneChild = destination.Nodes.FirstOrDefault(i => i.Equals(child));
                                cloneSequencerNode.ChildNodes.Add(cloneChild);
                            }

                            if (sequencerNode.Parent != null)
                            {
                                var cloneParent = destination.Nodes.FirstOrDefault(i => i.Equals(sequencerNode.Parent));
                                cloneSequencerNode.Parent = cloneParent;
                            }
                            else
                                cloneSequencerNode.Parent = null;
                            
                            break;
                        }
                        
                        var cloneCompositeNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode)) as CompositeNode;
                        foreach (var child in ((CompositeNode)originalNode).ChildNodes)
                        {
                            var cloneChild = destination.Nodes.FirstOrDefault(i => i.Equals(child));
                            cloneCompositeNode.ChildNodes.Add(cloneChild);
                        }

                        if (((CompositeNode) originalNode).Parent)
                        {
                            var cloneParentCompositeNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode.Parent));
                            cloneCompositeNode.Parent = cloneParentCompositeNode;
                        }
                        else 
                            cloneCompositeNode.Parent = null;
                     
                        
                        break;
                    case DecoratorNode:
                        RepeatNode repeatNode = originalNode as RepeatNode;
                        if (repeatNode)
                        {
                            var cloneRepeatNode = destination.Nodes.FirstOrDefault(i => i.Equals(repeatNode)) as RepeatNode;

                            if (repeatNode.Parent != null)
                            {
                                var cloneParent = destination.Nodes.FirstOrDefault(i => i.Equals(repeatNode.Parent));
                                cloneRepeatNode.Parent = cloneParent;  
                            }
                            else
                                cloneRepeatNode.Parent = null;
                            
                            if (repeatNode.Child != null)
                            {
                                var cloneChild = destination.Nodes.FirstOrDefault(i => i.Equals(repeatNode.Child));
                                cloneRepeatNode.Child = cloneChild;
                            }
                            else
                                cloneRepeatNode.Child = null;

                            if (repeatNode.ConditionNode != null)
                            {
                                var cloneConditionNode = destination.Nodes.FirstOrDefault(i => i.Equals(repeatNode.ConditionNode)) as ConditionNode;
                                cloneRepeatNode.ConditionNode = cloneConditionNode;
                            }
                            else
                                cloneRepeatNode.ConditionNode = null;
                            
                            break;
                        }
                        RootNode rootNode = originalNode as RootNode;
                        if (rootNode)
                        {
                            var cloneRootNode = destination.Nodes.FirstOrDefault(i => i.Equals(rootNode)) as RootNode;
                            if (rootNode.Child != null)
                            {
                                var cloneChild = destination.Nodes.FirstOrDefault(i => i.Equals(rootNode.Child));
                                cloneRootNode.Child = cloneChild;
                            }
                            else 
                                cloneRootNode.Child = null;

                            break;
                        }
                        
                        var cloneDecoratorNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode)) as DecoratorNode;

                        if (((DecoratorNode) originalNode).Child != null)
                        {
                            var cloneChidDecoratorNode = destination.Nodes.FirstOrDefault(i =>
                                i.Equals((originalNode as DecoratorNode).Child));
                            cloneDecoratorNode.Child = cloneChidDecoratorNode;
                        }
                        else
                            cloneDecoratorNode.Child = null;

                        if (((DecoratorNode) originalNode).Parent != null)
                        {
                            var cloneParentDecoratorNode = destination.Nodes.FirstOrDefault(i => i.Equals((originalNode as DecoratorNode).Parent));
                            cloneDecoratorNode.Parent = cloneParentDecoratorNode;
                        }
                        else
                            cloneDecoratorNode.Parent = null;

                        break;
                    case ParameterNode:
                        var cloneParameterNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode)) as ParameterNode;
                        if (((ParameterNode) originalNode).ChildNode != null)
                        {
                            var cloneChildParameterNode = destination.Nodes.FirstOrDefault(i => i.Equals((originalNode as ParameterNode).ChildNode));
                            cloneParameterNode.ChildNode = cloneChildParameterNode;
                        }
                        else cloneParameterNode.ChildNode = null;
                        break;
                    case ConditionNode:
                        var cloneCondition = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode)) as ConditionNode;
                        if (((ConditionNode) originalNode).ChildNode == null)
                        {
                            var cloneChildConditionNode = destination.Nodes.FirstOrDefault(i => i.Equals(((ConditionNode) originalNode).ChildNode));
                            cloneCondition.ChildNode = cloneChildConditionNode;
                        }
                        else
                            cloneCondition.ChildNode = null;
                        
                        break;
                    case Node:
                        var cloneNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode));
                        if (originalNode.Parent != null)
                        {
                            var cloneParentNode = destination.Nodes.FirstOrDefault(i => i.Equals(originalNode.Parent));
                            cloneNode.Parent = cloneParentNode;
                        }
                        else
                            cloneNode.Parent = null;
                        
                        break;
                }
            }
        }        
        public void SetCurrentNode(Node node)
        {
            if (_prevNode != null) _prevNode.State = State.Running;
            _prevNode = _currentNode;
            _currentNode = node;
        }

#if UNITY_EDITOR
        private void SaveAsset(UnityEngine.Object asset)
        {
            AssetDatabase.SaveAssetIfDirty(asset);
            EditorUtility.SetDirty(asset);
        }
        private void AddNodeToTree(Node node)
        {
            AssetDatabase.AddObjectToAsset(node, this);
            SaveAsset(this);
        }
        private void RemoveNodeFromTree(Node node)
        {
            AssetDatabase.RemoveObjectFromAsset(node);
            SaveAsset(this);
        }
        private void SaveNode(Node parentNode) => SaveAsset(parentNode);
        private void AddGroupToTree(GroupSO group)
        {
            AssetDatabase.AddObjectToAsset(group, this);
            SaveAsset(this);
        }
        private void RemoveGroupFromTree(GroupSO group)
        {
            AssetDatabase.RemoveObjectFromAsset(group);
            SaveAsset(this);
        }
        public void AddChild(Node parentNode, Node childNode)
        {
            DecoratorNode decoratorNode = parentNode as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.Child = childNode;
                childNode.Parent = decoratorNode;
            }

            CompositeNode compositeNode = parentNode as CompositeNode;
            if (compositeNode)
            {
                compositeNode.ChildNodes.Add(childNode);
                childNode.Parent = compositeNode;
            }

            ParameterNode parameterNode = parentNode as ParameterNode;
            if (parameterNode)
            {
                parameterNode.ChildNode = childNode;
                ChoiceNode choiceNode = childNode as ChoiceNode;
                if (choiceNode)
                    choiceNode.ParametersList.Add(parameterNode);
            }

            ConditionNode conditionNode = parentNode as ConditionNode;
            if (conditionNode)
            {
                conditionNode.ChildNode = childNode;

                RepeatNode repeatNode = childNode as RepeatNode;
                if (repeatNode)
                    repeatNode.ConditionNode = (ConditionNode)conditionNode;
            }

#if UNITY_EDITOR
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            SaveNode(parentNode);
#endif
        }
        public void RemoveChild(Node parentNode, Node childNode)
        {
            parentNode.RemoveChild(childNode);
            SaveNode(parentNode);
            SaveNode(childNode);
        }
        public Node CreateNode(Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.State = State.Running;
            node.name = type.Name;

#if UNITY_EDITOR
            node.GUID = GUID.Generate().ToString();
#endif

            Nodes.Add(node);

#if UNITY_EDITOR
            AddNodeToTree(node);
#endif
            return node;
        }
        public void RemoveNode(Node node)
        {
            if (node is RootNode) this.RootNode = null;
            Nodes.Remove(node);
            RemoveNodeFromTree(node);
        }
        public GroupSO CreateGroup(string title)
        {
            GroupSO groupSo = ScriptableObject.CreateInstance<GroupSO>();
            groupSo.Title = title;
            groupSo.name = title;
            groupSo.GUID = GUID.Generate().ToString();
            Groups.Add(groupSo);
            AddGroupToTree(groupSo);
            return groupSo;
        }
        public void RemoveGroup(GroupSO groupSo)
        {
            Groups.Remove(groupSo);
            RemoveGroupFromTree(groupSo);
        }
#endif
    }
}
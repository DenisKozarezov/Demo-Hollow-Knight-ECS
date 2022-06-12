/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.CompositeNodes;
using AI.BehaviorTree.Nodes.DecoratorNodes;
using Node = AI.BehaviorTree.Nodes.Node;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI.BehaviorTree
{
    public enum TreeOrientation
    {
        Vertical,
        Horizontal
    }

    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        [NonSerialized] public State TreeState;
        [SerializeField, HideInInspector] public Node RootNode;
        [SerializeField, HideInInspector] public List<Node> Nodes = new List<Node>();
        [SerializeField, HideInInspector] public List<GroupSO> Groups = new List<GroupSO>();
        [SerializeField, HideInInspector] public EntityReference EntityReference;
        [SerializeField, HideInInspector] public TreeOrientation OrientationTree = TreeOrientation.Horizontal;

        [NonSerialized] private Node _prevNode;
        [NonSerialized] private Node _currentNode;

        //инициализация
        public void Init(EcsWorld ecsWorld)
        {
            _currentNode = RootNode;

            foreach (var node in Nodes)
            {
                node.Init(this, ecsWorld);
            }
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

        private State GetState(Node node)
        {
            switch (node.State)
            {
                case State.Success: return State.Success;
                case State.Running: return _currentNode.Update();
                default:
                    SetCurrentNode(_currentNode.Parent);
                    return State.Running;
            }
        }

        public BehaviorTree Clone()
        {
            BehaviorTree clone = CloneNodes();
            CloneEdges(this, clone);
            return clone;
        }
        
        public BehaviorTree CloneNodes()
        {
            BehaviorTree clone = Instantiate(this);
            clone.Nodes = Nodes.ConvertAll(child => child.Clone());
            clone.RootNode = clone.Nodes.First(i => i is RootNode);
            return clone;
        }

        public void CloneEdges(BehaviorTree originalTree, BehaviorTree clone)
        {
            foreach (var node in clone.Nodes)
            {
                var originalNode = originalTree.Nodes.Find(i => i.GUID == node.GUID);
                switch (originalNode)
                {
                    case ActionNode:
                        var cloneActionNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((ActionNode) originalNode).GUID) as ActionNode;
                        if (((ActionNode) originalNode).Parent != null)
                        {
                            var cloneParentActionNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((ActionNode) originalNode).Parent.GUID);
                            cloneActionNode.Parent = cloneParentActionNode;
                        }
                        else 
                            cloneActionNode.Parent = null;
                        break;
                    case CompositeNode:
                        ChoiceNode choiceNode = originalNode as ChoiceNode;
                        if (choiceNode)
                        {
                            var cloneChoiceNode = clone.Nodes.FirstOrDefault(i => i.GUID == choiceNode.GUID) as ChoiceNode;
                            foreach (var parameterNode in choiceNode.ParametersList)
                            {
                                var cloneParameter = clone.Nodes.FirstOrDefault(i => i.GUID == parameterNode.GUID) as ParameterNode;
                                cloneParameter.ChildNode = cloneChoiceNode;
                                cloneChoiceNode.ParametersList.Add(cloneParameter);
                            }

                            foreach (var child in choiceNode.ChildNodes)
                            {
                                var cloneChild = clone.Nodes.FirstOrDefault(i => i.GUID == child.GUID);
                                cloneChoiceNode.ChildNodes.Add(cloneChild);
                            }

                            if (choiceNode.Parent != null)
                            {
                                var cloneParent = clone.Nodes.FirstOrDefault(i => i.GUID == choiceNode.Parent.GUID);
                                cloneChoiceNode.Parent = cloneParent;
                            }
                            else 
                                cloneChoiceNode.Parent = null;
                            
                            break;
                        }
                        SequencerNode sequencerNode = originalNode as SequencerNode;
                        if (sequencerNode)
                        {
                            var cloneSequencerNode = clone.Nodes.FirstOrDefault(i => i.GUID == sequencerNode.GUID) as SequencerNode;
                            foreach (var child in sequencerNode.ChildNodes)
                            {
                                var cloneChild = clone.Nodes.FirstOrDefault(i => i.GUID == child.GUID);
                                cloneSequencerNode.ChildNodes.Add(cloneChild);
                            }

                            if (sequencerNode.Parent != null)
                            {
                                var cloneParent = clone.Nodes.FirstOrDefault(i => i.GUID == sequencerNode.Parent.GUID);
                                cloneSequencerNode.Parent = cloneParent;
                            }
                            else
                                cloneSequencerNode.Parent = null;
                            
                            break;
                        }
                        
                        var cloneCompositeNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((CompositeNode)originalNode).GUID) as CompositeNode;
                        foreach (var child in ((CompositeNode)originalNode).ChildNodes)
                        {
                            var cloneChild = clone.Nodes.FirstOrDefault(i => i.GUID == child.GUID);
                            cloneCompositeNode.ChildNodes.Add(cloneChild);
                        }

                        if (((CompositeNode) originalNode).Parent)
                        {
                            var cloneParentCompositeNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((CompositeNode) originalNode).Parent.GUID);
                            cloneCompositeNode.Parent = cloneParentCompositeNode;
                        }
                        else 
                            cloneCompositeNode.Parent = null;
                     
                        
                        break;
                    case DecoratorNode:
                        RepeatNode repeatNode = originalNode as RepeatNode;
                        if (repeatNode)
                        {
                            var cloneRepeatNode = clone.Nodes.FirstOrDefault(i => i.GUID == repeatNode.GUID) as RepeatNode;

                            if (repeatNode.Parent != null)
                            {
                                var cloneParent = clone.Nodes.FirstOrDefault(i => i.GUID == repeatNode.Parent.GUID);
                                cloneRepeatNode.Parent = cloneParent;  
                            }
                            else
                                cloneRepeatNode.Parent = null;
                            
                            if (repeatNode.Child != null)
                            {
                                var cloneChild = clone.Nodes.FirstOrDefault(i => i.GUID == repeatNode.Child.GUID);
                                cloneRepeatNode.Child = cloneChild;
                            }
                            else
                                cloneRepeatNode.Child = null;

                            if (repeatNode.ConditionNode != null)
                            {
                                var cloneConditionNode = clone.Nodes.FirstOrDefault(i => i.GUID == repeatNode.ConditionNode.GUID) as ConditionNode;
                                cloneRepeatNode.ConditionNode = cloneConditionNode;
                            }
                            else
                                cloneRepeatNode.ConditionNode = null;
                            
                            break;
                        }
                        RootNode rootNode = originalNode as RootNode;
                        if (rootNode)
                        {
                            var cloneRootNode = clone.Nodes.FirstOrDefault(i => i.GUID == rootNode.GUID) as RootNode;
                            if (rootNode.Child != null)
                            {
                                var cloneChild = clone.Nodes.FirstOrDefault(i => i.GUID == rootNode.Child.GUID);
                                cloneRootNode.Child = cloneChild;
                            }
                            else 
                                cloneRootNode.Child = null;

                            break;
                        }
                        
                        var cloneDecoratorNode = clone.Nodes.FirstOrDefault(i => i.GUID == originalNode.GUID) as DecoratorNode;

                        if (((DecoratorNode) originalNode).Child != null)
                        {
                            var cloneChidDecoratorNode = clone.Nodes.FirstOrDefault(i =>
                                i.GUID == ((DecoratorNode) originalNode).Child.GUID);
                            cloneDecoratorNode.Child = cloneChidDecoratorNode;
                        }
                        else
                            cloneDecoratorNode.Child = null;

                        if (((DecoratorNode) originalNode).Parent != null)
                        {
                            var cloneParentDecoratorNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((DecoratorNode)originalNode).Parent.GUID);
                            cloneDecoratorNode.Parent = cloneParentDecoratorNode;
                        }
                        else
                            cloneDecoratorNode.Parent = null;

                        break;
                    case ParameterNode:
                        var cloneParameterNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((ParameterNode) originalNode).GUID) as ParameterNode;
                        if (((ParameterNode) originalNode).ChildNode != null)
                        {
                            var cloneChildParameterNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((ParameterNode) originalNode).ChildNode.GUID);
                            cloneParameterNode.ChildNode = cloneChildParameterNode;
                        }
                        else cloneParameterNode.ChildNode = null;
                        break;
                    case ConditionNode:
                        var cloneCondition = clone.Nodes.FirstOrDefault(i => i.GUID == ((ConditionNode) originalNode).GUID) as ConditionNode;
                        if (((ConditionNode) originalNode).ChildNode == null)
                        {
                            var cloneChildConditionNode = clone.Nodes.FirstOrDefault(i => i.GUID == ((ConditionNode) originalNode).ChildNode.GUID);
                            cloneCondition.ChildNode = cloneChildConditionNode;
                        }
                        else
                            cloneCondition.ChildNode = null;
                        
                        break;
                    case Node:
                        var cloneNode = clone.Nodes.FirstOrDefault(i => i.GUID == originalNode.GUID);
                        if (originalNode.Parent != null)
                        {
                            var cloneParentNode = clone.Nodes.FirstOrDefault(i => i.GUID == originalNode.Parent.GUID);
                            cloneNode.Parent = cloneParentNode;
                        }
                        else
                            cloneNode.Parent = null;
                        
                        break;
                }
            }
        }
        
        #region Node Manipulations
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
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            AddNodeToTree(node);
#endif
            return node;
        }
        public void RemoveNode(Node node)
        {
            if (node is RootNode)
                this.RootNode = null;

            Nodes.Remove(node);

#if UNITY_EDITOR
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            RemoveNodeFromTree(node);
#endif
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
            DecoratorNode decoratorNode = parentNode as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.Child.Parent = null;
                decoratorNode.Child = null;
            }

            CompositeNode compositeNode = parentNode as CompositeNode;
            if (compositeNode)
            {
                childNode.Parent = null;
                compositeNode.ChildNodes.Remove(childNode);
            }

            ParameterNode parameterNode = parentNode as ParameterNode;
            if (parameterNode)
            {
                parameterNode.ChildNode = null;

                ChoiceNode choiceNode = childNode as ChoiceNode;
                if (choiceNode)
                    choiceNode.ParametersList.Remove(parameterNode);
            }

            ConditionNode conditionNode = parentNode as ConditionNode;
            if (conditionNode)
            {
                conditionNode.ChildNode = null;

                RepeatNode repeatNode = childNode as RepeatNode;
                if (repeatNode)
                    repeatNode.ConditionNode = null;
            }

#if UNITY_EDITOR
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            SaveNode(parentNode);
            SaveNode(childNode);
#endif
        }
        public List<Node> GetChildren(Node parentNode)
        {
            List<Node> childern = new List<Node>();

            DecoratorNode decoratorNode = parentNode as DecoratorNode;
            if (decoratorNode && decoratorNode.Child != null)
                childern.Add(decoratorNode.Child);

            CompositeNode compositeNode = parentNode as CompositeNode;
            if (compositeNode && compositeNode.ChildNodes.Count > 0)
                return compositeNode.ChildNodes;

            ParameterNode parameterNode = parentNode as ParameterNode;
            if (parameterNode && parameterNode.ChildNode != null)
                childern.Add(parameterNode.ChildNode);

            ConditionNode conditionNode = parentNode as ConditionNode;
            if (conditionNode && conditionNode.ChildNode != null)
                childern.Add(conditionNode.ChildNode);

            SaveNode(parentNode); 

            return childern;
        }

        //Назначает узел текущим узлом для исполнения
        public void SetCurrentNode(Node node)
        {
            if (_prevNode != null)
                _prevNode.State = State.Running;
            _prevNode = _currentNode;
            _currentNode = node;
        }
        #endregion

        #region Group Manipulators
        public GroupSO CreateGroup(string title)
        {
            GroupSO groupSo = ScriptableObject.CreateInstance(typeof(GroupSO)) as GroupSO;
            groupSo.Title = title;
            groupSo.name = title;

#if UNITY_EDITOR
            groupSo.GUID = GUID.Generate().ToString();
#endif

            Groups.Add(groupSo);

#if UNITY_EDITOR
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            AddGroupToTree(groupSo);
#endif

            return groupSo;
        }
        public void RemoveGroup(GroupSO groupSo)
        {
            Groups.Remove(groupSo);

#if UNITY_EDITOR
            /************** ФИКСИРУЕМ ИЗМЕНЕНИЯ *************/
            RemoveGroupFromTree(groupSo);
#endif
        }
        #endregion

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
#endif
    }
}
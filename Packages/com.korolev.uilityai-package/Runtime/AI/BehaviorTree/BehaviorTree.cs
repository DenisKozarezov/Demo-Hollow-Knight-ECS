/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.CompositeNodes;
using AI.BehaviorTree.Nodes.DecoratorNodes;
using Leopotam.Ecs;
using UnityEngine;
using Node = AI.BehaviorTree.Nodes.Node;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AI.BehaviorTree
{
    public enum OrientationTree
    {
        Vertical,
        Horizontal
    }
    
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        [NonSerialized]  public State TreeState;
        [SerializeField] [HideInInspector] public Node RootNode;
        [SerializeField] [HideInInspector] public List<Node> Nodes = new List<Node>();
        [SerializeField] [HideInInspector] public List<GroupSO> Groups = new List<GroupSO>();
        [SerializeField] [HideInInspector] public GameObject GameObjectRef;
        [SerializeField] [HideInInspector] public OrientationTree OrientationTree = OrientationTree.Horizontal;

        [NonSerialized] private Node _prevNode;
        [NonSerialized] private Node _currentNode;
        
        //инициализация
        public void Init(EcsWorld ecsWorld)
        {
            _currentNode = RootNode;
            
            foreach (var node in Nodes)
            {
                node.BehaviorTreeRef = this;
                node.OnInit();
                node.OnInit(ecsWorld);
            }
        }
        
        public State Update()
        {
            //проход по всем узлам - параметрам
            List<Node> parameterNodes = Nodes.Where(n => n is ParameterNode).ToList();
            foreach (var node in parameterNodes) {
                ((ParameterNode)node).Update();
            }
            
            //проход по всем узлам - условиям
            List<Node> conditionNodes = Nodes.Where(n => n is ConditionNode).ToList();
            foreach (var node in conditionNodes) {
                ((ConditionNode)node).Update();
            }

            //проверки текущего состояния
            if (RootNode.State == State.Success || RootNode.State == State.Failure)
                return RootNode.State;

            if (_currentNode == null && _prevNode.State == State.Success)
                return State.Success;
            else if(_currentNode == null)
                return State.Failure;
            
            //обновление текущего узла
            if (_currentNode.State == State.Running)
                return _currentNode.Update();
            else
                SetCurrentNode(_currentNode.Parent);

            return State.Running;
        }
        
        #region Node Manipulations
            public Node CreateNode(System.Type type) {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.State = State.Running;
            node.name = type.Name;
            
            #if UNITY_EDITOR
                node.GUID = GUID.Generate().ToString();
            #endif
           
           Nodes.Add(node);
           
        #if UNITY_EDITOR
           /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/ 
                AssetDatabase.AddObjectToAsset(node, this);
                AssetDatabase.SaveAssetIfDirty	(this);
                EditorUtility.SetDirty(this);
        #endif
             return node;
        }
            public void RemoveNode(Node node) {
            if (node is RootNode)
                this.RootNode = null;
            
            Nodes.Remove(node);
            
        #if UNITY_EDITOR
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssetIfDirty	(this);
            EditorUtility.SetDirty(this);
        #endif
        }
            public void AddChild(Node parentNode, Node childNode) {
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
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/
            AssetDatabase.SaveAssetIfDirty	(parentNode);
            EditorUtility.SetDirty(parentNode);
        #endif
            
        }
            public void RemoveChild(Node parentNode, Node childNode) {
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
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/
            AssetDatabase.SaveAssetIfDirty	(parentNode);
            EditorUtility.SetDirty(parentNode);
            
            AssetDatabase.SaveAssetIfDirty	(childNode);
            EditorUtility.SetDirty(childNode);
        #endif
        }
            public List<Node> GetChildren(Node parentNode) {
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

        #if UNITY_EDITOR
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/
            AssetDatabase.SaveAssetIfDirty	(parentNode);
            EditorUtility.SetDirty(parentNode);
        #endif
            return childern;
        }
            
            //Назначает узел текущим узлом для исполнения
            public void SetCurrentNode(Node node)
            {
                if(_prevNode != null) 
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
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/ 
            AssetDatabase.AddObjectToAsset(groupSo, this);
            AssetDatabase.SaveAssetIfDirty	(this);
            EditorUtility.SetDirty(this);
        #endif
            return groupSo;
        }
            public void RemoveGroup(GroupSO groupSo) {
            Groups.Remove(groupSo);
            
        #if UNITY_EDITOR
            /************** ФИКОСИРУЕМ ИЗМЕНЕНИЯ *************/
            AssetDatabase.RemoveObjectFromAsset(groupSo);
            AssetDatabase.SaveAssetIfDirty	(this);
            EditorUtility.SetDirty(this);
        #endif
        }
        #endregion
    }
}
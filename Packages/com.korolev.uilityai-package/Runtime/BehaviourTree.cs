using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Node = BehaviourTree.Runtime.Nodes.Node;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
#endif

namespace BehaviourTree.Runtime
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        private State _currentState = State.Running;

        [SerializeField, HideInInspector]
        public Node RootNode;
        [SerializeField, HideInInspector]
        private List<Node> _nodes = new List<Node>();
        
        public IReadOnlyCollection<Node> Nodes => _nodes;

        private void TraverseTree(Node node, Action<Node> visiter)
        {
            if (node)
            {
                visiter.Invoke(node);
                foreach (Node child in node.GetChildren())
                {
                    TraverseTree(child, visiter);
                }
            }
        }
        public void Init(Entitas.IEntity agent)
        {
            foreach (Node node in _nodes) node.Init(agent);
        }
        public State Update()
        {
            if (RootNode.State == State.Running)
            {
                _currentState = RootNode.Update();
            }
            return _currentState;
        }
        public BehaviourTree Clone()
        {
            BehaviourTree clone = Instantiate(this);
            clone.RootNode = clone.RootNode.Clone();
            clone._nodes = new List<Node>();
            TraverseTree(clone.RootNode, (node) => clone._nodes.Add(node));
            return clone;
        }

#if UNITY_EDITOR
        [Header("Tree Settings")]
        [SerializeField]
        private Orientation _orientation;
        [SerializeField]
        private bool _enableRuntimeEdit;
        public Orientation Orientation => _orientation;
        public bool EnableRuntimeEdit => _enableRuntimeEdit;

        private void AddObjectToAsset(Node node)
        {
            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            AssetDatabase.SaveAssets();
        }
        private void RemoveObjectFromAsset(Node node)
        {
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }
        public Node CreateNode(Type type, string displayName)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.Name = displayName;
            node.GUID = GUID.Generate().ToString();

            Undo.RecordObject(this, "Create Node (Behaviour Tree)");
            _nodes.Add(node);
            Undo.RegisterCreatedObjectUndo(node, "Create Node (Behaviour Tree)");

            AddObjectToAsset(node);
            return node;
        }
        public void RemoveNode(Node node)
        {
            Undo.RecordObject(this, "Remove Node (Behaviour Tree)");
            _nodes.Remove(node);
            RemoveObjectFromAsset(node);

            if (node is Root) RootNode = null;
        }
#endif
    }
}
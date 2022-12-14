using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
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
        public EcsEntity Agent;

        public void Init(ref EcsEntity agent)
        {
            Agent = agent;            
            foreach (var node in Nodes) node.Init(this);
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
            BehaviourTree tree = Instantiate(this);
            tree.RootNode = tree.RootNode.Clone();
            return tree;
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

            if (node is RootNode) RootNode = null;
        }
#endif
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree.Runtime.Nodes
{
    public abstract class CompositeNode : Node
    {
        protected int _currentIndex;
        [SerializeField, HideInInspector] 
        public List<Node> Children = new List<Node>();

        protected bool AllChildrenEnumerated => _currentIndex == Children.Count;

        protected override void OnStart()
        {
            _currentIndex = 0;
        }
        protected override void OnInit() { }
        protected override void OnStop() { }
        public override Node Clone()
        {
            CompositeNode clone = Instantiate(this);
            clone.Children = Children.ConvertAll(c => c.Clone());
            return clone;
        }

#if UNITY_EDITOR
        public sealed override IEnumerable<Node> GetChildren()
        {
            return Children;
        }
        public sealed override void AddChild(Node node)
        {
            if (node == null) return;
            if (!Children.Contains(node))
            {
                UnityEditor.Undo.RecordObject(this, "Add Child (Behaviour Tree)");
                Children.Add(node);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
        public sealed override void RemoveChild(Node node)
        {
            if (node == null) return;
            UnityEditor.Undo.RecordObject(this, "Remove Child (Behaviour Tree)");
            Children.Remove(node);
            UnityEditor.EditorUtility.SetDirty(this);
        }
        public void SortChildren(System.Comparison<Node> comparer)
        {
            Children?.Sort(comparer);
        }
#endif
    }  
}

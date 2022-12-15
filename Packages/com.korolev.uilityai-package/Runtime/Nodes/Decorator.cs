using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree.Runtime.Nodes
{
    public abstract class Decorator : Node
    {
        [SerializeField, HideInInspector] public Node Child;

        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public override Node Clone()
        {
            Decorator clone = Instantiate(this);
            if (Child != null) clone.Child = Child.Clone();
            return clone;
        }

#if UNITY_EDITOR
        public sealed override IEnumerable<Node> GetChildren()
        {
            if (Child == null) yield break;
            yield return Child;
        }
        public sealed override void AddChild(Node node)
        {
            UnityEditor.Undo.RecordObject(this, "Add Child (Behaviour Tree)");
            Child = node;
            UnityEditor.EditorUtility.SetDirty(this);
        }
        public sealed override void RemoveChild(Node node)
        {
            UnityEditor.Undo.RecordObject(this, "Remove Child (Behaviour Tree)");
            Child = null;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}

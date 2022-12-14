using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree.Nodes
{
    public abstract class ConditionNode : Node
    {
        [HideInInspector] public Node ChildNode;

        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public abstract bool Condition();
        public override IEnumerable<Node> GetChildren()
        {
            yield return ChildNode;
        }
        public override void RemoveChild(Node child)
        {
            if (!child.Equals(ChildNode)) return;

            child.Parent = null;
            ChildNode = null;
        }
        public override Node Clone()
        {
            ConditionNode clone = Instantiate(this);
            clone.GUID = GUID;
            clone.Parent = null;
            clone.ChildNode = null;
            return clone;
        }
    }
}
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class ConditionNode : Node
    {
        [HideInInspector] public Node ChildNode;

        public abstract bool Condition();
        public override Node Clone()
        {
            ConditionNode other = Instantiate(this);
            other.ChildNode = other.ChildNode.Clone();
            return other;
        }
    }
}
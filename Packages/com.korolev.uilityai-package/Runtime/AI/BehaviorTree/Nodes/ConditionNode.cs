using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class ConditionNode : Node
    {
        [HideInInspector] public Node ChildNode;
        public abstract bool Condition();
    }
}
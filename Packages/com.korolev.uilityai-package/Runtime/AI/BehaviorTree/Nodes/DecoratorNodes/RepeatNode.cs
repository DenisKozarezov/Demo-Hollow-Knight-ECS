using UnityEngine;

namespace AI.BehaviourTree.Nodes.Decorators
{
    public class RepeatNode : DecoratorNode
    {
        [HideInInspector] public ConditionNode ConditionNode;

        protected override State OnUpdate()
        {
            if (ConditionNode == null || ConditionNode.Condition())
            {
                BehaviourTreeRef.SetCurrentNode(Child);
                return State.Running; 
            }
            return State.Success;
        }
        public override void RemoveChild(Node child)
        {
            base.RemoveChild(child);
            ConditionNode = null;
        }
        public override Node Clone()
        {
            RepeatNode clone = base.Clone() as RepeatNode;
            clone.ConditionNode = null;
            return clone;
        }
    }
}
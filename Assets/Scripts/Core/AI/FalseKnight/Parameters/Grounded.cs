using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Parameters
{
    public class Grounded : BooleanNode
    {
        protected override State OnUpdate()
        {
            Value = BehaviorTreeRef.Agent.Has<OnGroundComponent>();
            return State.Success;
        }
        public override float Cost()
        {
            return Value ? 1f : 0f;
        }
    }
}
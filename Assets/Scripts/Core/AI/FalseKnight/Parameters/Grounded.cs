using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.AI.FalseKnight.Parameters
{
    public class Grounded : BooleanNode
    {
        protected override State OnUpdate()
        {
            Value = BehaviorTreeRef.EntityReference.Entity.Has<OnGroundComponent>();
            return State.Success;
        }
    }
}
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Examples.Example_1.ECS;
using Leopotam.Ecs;

namespace Examples.Example_1.FalseKnight.AI.Parameters
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
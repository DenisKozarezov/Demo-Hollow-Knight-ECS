using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Examples.Example_1.ECS;
using Examples.Example_1.ECS.ComponentProviders;
using Leopotam.Ecs;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class Grounded : BooleanNode
    {
        private EntityReference _entityReference;
        protected override void OnStart()
        {
            _entityReference = BehaviorTreeRef.GameObjectRef.GetComponent<EntityReference>();         
        }
        protected override State OnUpdate()
        {
            Value = _entityReference.Entity.Has<OnGroundComponent>();
            return State.Success;
        }
    }
}
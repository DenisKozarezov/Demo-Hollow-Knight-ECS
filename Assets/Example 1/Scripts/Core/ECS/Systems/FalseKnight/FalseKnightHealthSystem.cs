using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Examples.Example_1.ECS.FalseKnight;
using Leopotam.Ecs;


namespace Examples.Example_1.ECS.Systems.FalseKnight
{
    public class FalseKnightHealthSystem : FloatNode, IEcsSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<FalseKnightHealthComponent> _filter = null;

        public override State OnUpdate()
        {
            ref var ecsEntity = ref _filter.GetEntity (0);
            this.Value = ecsEntity.Get<FalseKnightHealthComponent>().Health;
            return State.Success;
        }
    }
}
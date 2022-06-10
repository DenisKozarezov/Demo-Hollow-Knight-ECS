using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems
{
    internal sealed class UnitInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitInitComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var component = ref entity.Get<UnitInitComponent>();

                // Set entity reference
                component.Value.Entity = entity;
            }
        }
    }
}
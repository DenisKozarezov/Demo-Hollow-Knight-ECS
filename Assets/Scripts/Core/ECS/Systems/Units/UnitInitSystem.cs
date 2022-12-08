using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    public sealed class UnitInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitInitComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var initComponent = ref entity.Get<UnitInitComponent>();
                initComponent.EntityReference.Entity = entity;
            }
        }
    }
}
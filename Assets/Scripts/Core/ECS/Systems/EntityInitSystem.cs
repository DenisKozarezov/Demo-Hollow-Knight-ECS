using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    public sealed class EntityInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EntityInitComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var initComponent = ref entity.Get<EntityInitComponent>();
                initComponent.EntityReference.Entity = entity;
            }
        }
    }
}
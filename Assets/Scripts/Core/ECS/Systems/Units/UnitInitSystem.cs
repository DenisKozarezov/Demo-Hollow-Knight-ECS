using Leopotam.Ecs;
using UnityEngine;

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
                ref var initComponent = ref entity.Get<UnitInitComponent>();
                ref var movable = ref entity.Get<MovableComponent>();
                ref var health = ref entity.Get<HealthComponent>();

                // Set entity reference
                initComponent.EntityReference.Entity = entity;
                movable.Value = initComponent.UnitModel.MovementSpeed;
                health.MaxHealth = health.Health = initComponent.UnitModel.MaxHealth;
            }
        }
    }
}
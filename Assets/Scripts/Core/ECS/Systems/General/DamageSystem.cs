using System;
using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, DamageEventComponent>
            .Exclude<DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var health = ref _filter.Get1(i);
                ref var damage = ref _filter.Get2(i);

                // If damage is zero then delete immediately
                if (damage.Damage == 0)
                {
                    entity.Del<DamageEventComponent>();
                    continue;
                }

                // Apply damage
                health.Health = Math.Max(health.Health - damage.Damage, 0);
                
                // Make the enemy red
                entity.Get<AnimateDamageEventComponent>().GameObjectRef = damage.Target;

#if UNITY_EDITOR
                Debug.Log($"Unit <b><color=yellow>{damage.Target.name}</color></b> recieved <b><color=red>{damage.Damage}</color></b> damage from <b><color=yellow>{damage.Source.name}</color></b>. Current health: <b><color=green>{health.Health}</color></b>.");
#endif
            }
        }
    }
}

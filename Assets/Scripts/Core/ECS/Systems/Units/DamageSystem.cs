using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, DamageEventComponent>
            .Exclude<InvulnerableComponent>
            .Exclude<DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var healthComponent = ref _filter.Get1(i);
                ref var damageComponent = ref _filter.Get2(i);

                // If damage is zero then delete immediately
                if (damageComponent.Damage == 0f)
                {
                    entity.Del<DamageEventComponent>();
                    continue;
                }

                // Apply damage
                if (healthComponent.Health - damageComponent.Damage > 0)
                {
                    healthComponent.Health -= damageComponent.Damage;

#if UNITY_EDITOR
                    Debug.Log($"Unit <b><color=yellow>{damageComponent.Target.name}</color></b> recieved damage <b><color=red>{damageComponent.Damage}</color></b> from <b><color=yellow>{damageComponent.Source.name}</color></b>. Current health: <b><color=green>{healthComponent.Health}</color></b>.");
#endif

                    // Make the enemy red
                    entity.Get<AnimateDamageEventComponent>().GameObjectRef = damageComponent.Target;
                }
                else healthComponent.Health = 0; 
            }
        }
    }
}

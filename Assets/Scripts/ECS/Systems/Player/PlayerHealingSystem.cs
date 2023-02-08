using System;
using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems
{
    public sealed class PlayerHealingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerHealedEvent> _filter = null;
        private readonly EcsFilter<HealthComponent>.Exclude<DiedComponent> _player = null;

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _filter)
            {
                foreach (var i in _player)
                {
                    ref var heal = ref _filter.Get1(i);
                    ref var health = ref _player.Get1(i);

                    if (heal.Value == 0 || health.Health >= health.MaxHealth)
                    {
                        continue;
                    }

                    // Heal
                    health.Health = Math.Min(health.Health + heal.Value, health.MaxHealth);

#if UNITY_EDITOR
                    Debug.Log($"<b><color=yellow>Player</color></b> restored <b><color=green>{heal.Value}</color></b> health point.");
#endif
                }
            }
        }
    }
}

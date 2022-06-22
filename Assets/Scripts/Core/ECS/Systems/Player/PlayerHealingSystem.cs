using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems
{
    internal sealed class PlayerHealingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, PlayerHealedEvent>
            .Exclude<DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var health = ref _filter.Get1(i);
                ref var heal = ref _filter.Get2(i);

                // If damage is zero then delete immediately
                if (heal.Value == 0 || health.Health >= health.MaxHealth)
                {
                    entity.Del<PlayerHealedEvent>();
                    continue;
                }

                // Heal
                health.Health += heal.Value;

#if UNITY_EDITOR
                Debug.Log($"<b><color=yellow>Player</color></b> healed <b><color=green>{heal.Value}</color></b> health point.");
#endif
            }
        }
    }
}

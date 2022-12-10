using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    public class HealthHealedUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerHealedEvent> _event = null;
        private readonly EcsFilter<HealthViewComponent> _hp = null;

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _event)
            {
                foreach (var hp in _hp)
                {
                    ref var healthView = ref _hp.Get1(hp);
                    ref int heal = ref _event.Get1(@event).Value;
                    healthView.View.RestoreHealth(heal);
                }
            }
        }
    }
}

using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    internal class HealthViewHealedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerHealedEvent> _filter = null;
        private readonly EcsFilter<HealthViewComponent> _hp = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                foreach (var hp in _hp)
                {
                    ref var healthView = ref _hp.Get1(hp);
                    ref var heal = ref _filter.Get1(i);

                    healthView.HealthView.AddHealth(heal.Value);
                }
            }
        }
    }
}

using Leopotam.Ecs;
using Core.ECS.Components.Player;
using Core.ECS.Components.UI;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.UI
{
    internal class HealthViewInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<HealthViewComponent> _healthView = null;
        private readonly EcsFilter<HealthComponent, PlayerTagComponent> _player = null;
        
        public void Init()
        {
            foreach (var player in _player)
            {
                foreach (var i in _healthView)
                {
                    var healthView = _healthView.Get1(i).View;
                    ref var health = ref _player.Get1(player);
                    healthView.Clear();
                    healthView.Init(health.MaxHealth);
                }
            }
        }
    }
}

using System;
using Core.ECS.Components.Player;
using Core.ECS.Components.UI;
using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    internal class HealthViewInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<HealthViewComponent> _filter = null;
        private readonly EcsFilter<HealthComponent, PlayerTagComponent> _player = null;
        
        public void Init()
        {
            foreach (var player in _player)
            {
                foreach (var i in _filter)
                {
                    var healthView = _filter.Get1(i).HealthView;
                    healthView.Clear();
                    healthView.AddHealth(5);
                }
            }
        }
    }
}

using Leopotam.Ecs;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.UI;

namespace Core.ECS.Systems.UI
{
    public sealed class HealthViewInitSystem /*: IEcsInitSystem*/
    {
        //private readonly EcsFilter<HealthComponent, PlayerTagComponent> _player = null;
        //private readonly HealthUIView _view;
        
        //public HealthViewInitSystem(HealthUIView view)
        //{
        //    _view = view;
        //}

        //void IEcsInitSystem.Init()
        //{
        //    foreach (var player in _player)
        //    {
        //        ref int maxHealth = ref _player.Get1(player).MaxHealth;
        //        _view.Clear();
        //        _view.Init(maxHealth);
        //    }
        //}
    }
}

using Core.Models;
using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems.FalseKnight
{
    internal class PlayerInitSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<UnitInitComponent, PlayerTagComponent> _filter = null;
        private PlayerModel _playerModel;

        public PlayerInitSystem(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                entity
                    .Replace(new HealthComponent
                    {
                        Health = _playerModel.MaxHealth,
                        MaxHealth = _playerModel.MaxHealth
                    })
                    .Replace(new MovableComponent { Value = _playerModel.MovementSpeed })
                    .Replace(new DamageComponent { Value = _playerModel.BaseDamage })
                    .Replace(new JumpComponent { Value = _playerModel.JumpForce });
            }            
        }
    }
}
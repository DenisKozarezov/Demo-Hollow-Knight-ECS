using Core.Models;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.ECS.Systems.FalseKnight
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
                    .Replace(new DamageComponent { Value = _playerModel.BaseDamage })
                    .Replace(new JumpComponent { JumpForceRange = _playerModel.JumpForceRange })
                    .Replace(new MovableComponent { Value = _playerModel.MovementSpeed });
            }            
        }
    }
}
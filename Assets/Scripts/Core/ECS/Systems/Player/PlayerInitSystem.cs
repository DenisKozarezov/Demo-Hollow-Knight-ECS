using Core.Models;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.ECS.Systems.FalseKnight
{
    internal class PlayerInitSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<UnitInitComponent, PlayerTagComponent> _filter = null;
        private readonly PlayerModel _playerModel;

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
                    .Replace(new DamageComponent { Damage = _playerModel.BaseDamage, AttackRange = _playerModel.AttackRange })
                    .Replace(new JumpComponent { JumpForceRange = _playerModel.JumpForceRange })
                    .Replace(new MovableComponent { Value = _playerModel.MovementSpeed })
                    .Replace(new AttackCooldownComponent { Value = _playerModel.AttackCooldown });
            }            
        }
    }
}
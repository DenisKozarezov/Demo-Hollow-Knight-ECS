using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.Models;

namespace Core.ECS.Systems.FalseKnight
{
    internal class FalseKnightInitSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<UnitInitComponent, FalseKnightTagComponent> _filter = null;
        private readonly FalseKnightModel _unitModel;

        public FalseKnightInitSystem(FalseKnightModel unitModel)
        {
            _unitModel = unitModel;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                entity
                    .Replace(new HealthComponent
                    {
                        Health = _unitModel.MaxHealth,
                        MaxHealth = _unitModel.MaxHealth
                    })
                    .Replace(new DamageComponent { Damage = _unitModel.BaseDamage, AttackRange = _unitModel.AttackRange })
                    .Replace(new JumpComponent { JumpForceRange = new Vector2(_unitModel.JumpHeight, _unitModel.JumpHeight) })
                    .Replace(new MovableComponent { Value = _unitModel.MovementSpeed });
            }            
        }
    }
}
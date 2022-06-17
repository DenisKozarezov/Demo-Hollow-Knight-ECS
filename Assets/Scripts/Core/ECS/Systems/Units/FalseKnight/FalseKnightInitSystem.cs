using Core.ECS.Components.Units;
using Core.Models;
using Leopotam.Ecs;

namespace Core.ECS.Systems.FalseKnight
{
    internal class FalseKnightInitSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<UnitInitComponent, FalseKnightTagComponent> _filter = null;
        private UnitModel _unitModel;

        public FalseKnightInitSystem(UnitModel unitModel)
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
                    .Replace(new DamageComponent { Value = _unitModel.BaseDamage })
                    .Replace(new JumpComponent { Value = _unitModel.JumpForce })
                    .Replace(new MovableComponent { Value = _unitModel.MovementSpeed });
            }            
        }
    }
}
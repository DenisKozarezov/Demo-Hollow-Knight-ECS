using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Events.FalseKnight;
using Core.Models;

namespace Core.ECS.Systems.FalseKnight
{
    internal class FalseKnightAttackAnimationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AnimatorComponent, ColliderComponent, FalseKnightAttackEventComponent>.Exclude<DiedComponent> _filter = null;

        private readonly FalseKnightModel _unitModel;
        internal FalseKnightAttackAnimationSystem(FalseKnightModel model)
        {
            _unitModel = model;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {         
                ref var ecsEntity = ref _filter.GetEntity(i);
                ref var animator = ref ecsEntity.Get<AnimatorComponent>().Value;
                ref var collider = ref ecsEntity.Get<ColliderComponent>().Value;
                animator.SetTrigger("Attack");
                ecsEntity.Del<FalseKnightAttackEventComponent>();

                // Hit enemies
                ref var hit = ref _world.NewEntity().Get<HitEventComponent>();
                hit.HitPosition = collider.bounds.center;
                hit.HitRadius = _unitModel.AttackRange;
                hit.TargetLayer = Constants.PlayerLayer;
                hit.Damage = _unitModel.BaseDamage;
                hit.Source = animator.gameObject;
               
                // Camera Shake
                _world.NewEntity().Get<AnimateCameraShakeEventComponent>();
            }
        }
    }
}
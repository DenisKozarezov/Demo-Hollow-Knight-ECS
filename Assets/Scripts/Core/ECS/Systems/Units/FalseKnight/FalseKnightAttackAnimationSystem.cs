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
        private readonly EcsFilter<
            AnimatorComponent, 
            DamageComponent,
            FalseKnightAttackEventComponent>.Exclude<DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {         
                ref var ecsEntity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get1(i).Value;
                ref var damage = ref _filter.Get2(i);

                animator.SetTrigger("Attack");
                ecsEntity.Del<FalseKnightAttackEventComponent>();

                // Hit enemies
                ref var hit = ref _world.NewEntity().Get<HitEventComponent>();
                hit.HitPosition = animator.transform.position;
                hit.HitRadius = damage.AttackRange;
                hit.TargetLayer = Constants.PlayerLayer;
                hit.Damage = damage.Damage;
                hit.Source = animator.gameObject;
               
                // Camera Shake
                _world.NewEntity().Get<AnimateCameraShakeEventComponent>().ShakeDuration = 0.3f;
            }
        }
    }
}
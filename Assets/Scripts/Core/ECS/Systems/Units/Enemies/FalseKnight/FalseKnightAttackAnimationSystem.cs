using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Events.FalseKnight;

namespace Core.ECS.Systems.FalseKnight
{
    public sealed class FalseKnightAttackAnimationSystem : IEcsRunSystem
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
                _world.NewEntity(new HitEventComponent
                {
                    HitPosition = animator.transform.position,
                    HitRadius = damage.AttackRange,
                    TargetLayer = Constants.PlayerLayer,
                    Damage = damage.Damage,
                    Source = animator.gameObject
                });

                // Camera Shake
                _world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 0.3f, ShakeForce = 0.2f });
            }
        }
    }
}
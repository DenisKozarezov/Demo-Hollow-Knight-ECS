using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Events.FalseKnight;

namespace Core.ECS.Systems.FalseKnight
{
    internal class FalseKnightAttackAnimationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<FalseKnightAttackEventComponent>.Exclude<DiedComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {         
                ref var ecsEntity = ref _filter.GetEntity(i);
                ecsEntity.Get<FalseKnightAttackEventComponent>().Animator.SetTrigger("Attack");
                ecsEntity.Del<FalseKnightAttackEventComponent>();
               
                // Camera Shake
                _world.NewEntity().Get<AnimateCameraShakeEventComponent>();
            }
        }
    }
}
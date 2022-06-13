using Leopotam.Ecs;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.Events.FalseKnight;

namespace Examples.Example_1.ECS.Systems.FalseKnight
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
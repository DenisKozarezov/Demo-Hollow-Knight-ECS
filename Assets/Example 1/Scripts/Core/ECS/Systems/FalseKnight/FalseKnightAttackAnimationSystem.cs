using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.Events.FalseKnight;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.ECS.Systems.FalseKnight
{
    internal class FalseKnightAttackAnimationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<FalseKnightAttackEventComponent>.Exclude<DiedComponent> _filter = null;
        
        private void Attack()
        {

        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                EcsEntity cameraShakeAnimationEntity = _world.NewEntity();
                
                ref var ecsEntity = ref _filter.GetEntity(i);
                ecsEntity.Get<FalseKnightAttackEventComponent>().GameObjectRef.GetComponent<Animator>().SetTrigger("Attack");
                ecsEntity.Del<FalseKnightAttackEventComponent>();
               
                cameraShakeAnimationEntity.Get<AnimateCameraShakeEventComponent>();
            }
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.FalseKnight;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems.FalseKnight
{
    internal class FalseKnightJumpAnimationSystem: IEcsRunSystem, IEcsSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<FalseKnightAnimationComponent, RigidbodyComponent>.Exclude<DiedComponent> _filter = null;

        // ==== ANIMATIONS KEYS ===
        private const string JUMP_KEY = "Jump";
        private const string JUMPING_KEY = "IsJumping";
        private const string LAND_KEY = "Land";
        private const string ATTACK_KEY = "Attack";
        // ========================

        private void OnAttack()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity (i);
                ecsEntity.Get<FalseKnightAnimationComponent>().Animator.SetTrigger(ATTACK_KEY);
            }  
        }
        
        private void OnMove(InputAction.CallbackContext context)
        {
           
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var animatorComponent = ref _filter.Get1(i);
                ref var rigidbodyComponent = ref _filter.Get2(i);

                bool onGround = entity.Has<OnGroundComponent>();
                bool isFalling = rigidbodyComponent.Value.velocity.y < 0;

                Animator animator = animatorComponent.Animator;

                if (isFalling)
                {
                    if (animator.GetBool(JUMPING_KEY) == false)
                    {
                        animator.SetTrigger(JUMP_KEY);
                        animator.SetBool(JUMPING_KEY, true);
                    }
                }
                else if (animator.GetBool(JUMPING_KEY) && onGround)
                {              
                    animator.SetBool(JUMPING_KEY, false);
                    animator.SetTrigger(LAND_KEY);

                    // Dust effect
                    ref Vector2 point = ref entity.Get<OnGroundComponent>().Point;
                    EcsEntity dustAnimationEntity = _world.NewEntity();             
                    dustAnimationEntity.Get<AnimateDustEventComponent>().Point = point;
                    dustAnimationEntity.Get<AnimateDustEventComponent>().Scale = new Vector3(1f, 1f, 1f);

                    // Shake camera when landed
                    EcsEntity cameraShakeAnimationEntity = _world.NewEntity();
                    cameraShakeAnimationEntity.Get<AnimateCameraShakeEventComponent>();
                }
            }
        }
    }
}
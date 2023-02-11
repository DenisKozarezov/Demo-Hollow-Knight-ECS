using UnityEngine;
using Core.ECS.Events;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.FalseKnight
{
    public sealed class FalseKnightJumpAnimationSystem/*: IEcsRunSystem*/
    {
        //private readonly EcsWorld _world = null;
        //private readonly EcsFilter<AnimatorComponent, RigidbodyComponent, FalseKnightTagComponent>
        //    .Exclude<DiedComponent> _filter = null;

        //// ==== ANIMATIONS KEYS ===
        //private const string JUMP_KEY = "Jump";
        //private const string JUMPING_KEY = "IsJumping";
        //private const string LAND_KEY = "Land";
        //// ========================

        //public void Run()
        //{
        //    foreach (var i in _filter)
        //    {
        //        ref EcsEntity entity = ref _filter.GetEntity(i);
        //        Animator animator = _filter.Get1(i).Value;
        //        bool onGround = entity.Has<OnGroundComponent>();

        //        if (!onGround)
        //        {
        //            if (animator.GetBool(JUMPING_KEY) == false)
        //            {
        //                animator.SetTrigger(JUMP_KEY);
        //                animator.SetBool(JUMPING_KEY, true);
        //            }
        //        }
        //        else if (animator.GetBool(JUMPING_KEY) && onGround)
        //        {
        //            animator.SetBool(JUMPING_KEY, false);
        //            animator.SetTrigger(LAND_KEY);

        //            // Create dust effect 
        //            _world.NewEntity(new CreateDustEventComponent 
        //            { 
        //                Point = entity.Get<OnGroundComponent>().Point,
        //                Scale = Vector3.one
        //            });

        //            // Shake camera when landed
        //            _world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 0.3f, ShakeForce = 0.2f });
        //        }
        //    }
        //}
    }
}
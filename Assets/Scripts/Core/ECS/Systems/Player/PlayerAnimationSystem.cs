using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Components;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimatorComponent, PlayerTagComponent> _filter = null;
       
        private readonly IInputSystem _playerInput;

        // ==== ANIMATIONS KEYS ===
        private const string FALL_KEY = "IsJumping";
        private const string JUMP_KEY = "Jump";
        private const string MOVE_KEY = "Move";
        private const string GROUND_KEY = "OnGround";
        // ========================

        internal PlayerAnimationSystem(IInputSystem playerInput) 
        { 
            _playerInput = playerInput; 
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                Animator animator = _filter.Get1(i).Value;
                bool onGround = entity.Has<OnGroundComponent>();
                bool channelling = entity.Has<ChannellingComponent>();

                // Falling and Jumping
                if (!onGround)
                {
                    if (!animator.GetBool(FALL_KEY))
                    {
                        animator.SetBool(FALL_KEY, true);
                        animator.SetTrigger(JUMP_KEY);
                    }
                }
                else animator.SetBool(FALL_KEY, false);

                // On Ground
                animator.SetBool(GROUND_KEY, onGround);

                // Movement
                animator.SetBool(MOVE_KEY, _playerInput.IsMoving && onGround && !channelling);
            }
        }
    }
}
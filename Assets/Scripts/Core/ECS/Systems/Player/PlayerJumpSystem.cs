using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Components;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerJumpSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<
            RigidbodyComponent, 
            JumpComponent, 
            OnGroundComponent, 
            PlayerTagComponent>
            .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        private readonly IInputSystem _playerInput;
              
        internal PlayerJumpSystem(IInputSystem playerInput) 
        {
            _playerInput = playerInput;
        }
        
        public virtual void Init() 
        {
            _playerInput.Jump += OnJump;        
        }
        public void Destroy()
        {
            _playerInput.Jump -= OnJump;
        }

        private void OnJump() 
        {
            foreach (var i in _filter)
            {
                Rigidbody2D rigidbody = _filter.Get1(i).Value;
                float jumpHeight = _filter.Get2(i).JumpForceRange.x;
                float jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }   
        private float CalculateJumpForce(float gravity, float height)
        {
            return Mathf.Sqrt(2 * gravity * height);
        }
    }
}
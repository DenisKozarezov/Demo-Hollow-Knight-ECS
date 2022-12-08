using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public class PlayerJumpSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<
            RigidbodyComponent, 
            JumpComponent, 
            OnGroundComponent,
            PlayerTagComponent>
            .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        private readonly IInputSystem _playerInput;
              
        public PlayerJumpSystem(IInputSystem playerInput) 
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
                float jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
                rigidbody.velocity += Vector2.up * jumpForce;
            }
        }
    }
}
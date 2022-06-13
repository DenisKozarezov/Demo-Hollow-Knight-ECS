using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Examples.Example_1.ECS.Components.Player;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerJumpSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<RigidbodyComponent, JumpComponent, PlayerTagComponent> _filter = null;

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
                var entity = _filter.GetEntity(i);
                Rigidbody2D rigidbody = entity.Get<RigidbodyComponent>().Value;
                float jumpForce = entity.Get<JumpComponent>().Value;

                bool onGround = entity.Has<OnGroundComponent>();

                if (onGround)
                {
                    rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }   
    }
}
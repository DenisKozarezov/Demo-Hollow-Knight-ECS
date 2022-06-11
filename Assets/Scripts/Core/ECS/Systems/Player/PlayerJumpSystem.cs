using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerJumpSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<RigidbodyComponent, PlayerJumpComponent> _filter = null;

        private readonly PlayerInputController _playerInput;
        private Rigidbody2D _rigidbody;

        private EcsEntity _entity;

        private const float JumpImpulse = 8;
        private bool IsFalling => !_entity.Has<OnGroundComponent>();

        internal PlayerJumpSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init() 
        {
            // Input
            _playerInput.Keyboard.Jump.performed += OnJump;

            // Initialize references
            _entity = _filter.GetEntity(0);
            _rigidbody = _entity.Get<RigidbodyComponent>().Value;         
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Jump.performed -= OnJump;
        }

        private void OnJump(InputAction.CallbackContext context) 
        {
            if (!IsFalling)
            {
                _rigidbody.AddForce(new Vector2(0, JumpImpulse), ForceMode2D.Impulse);
            }
        }   
    }
}
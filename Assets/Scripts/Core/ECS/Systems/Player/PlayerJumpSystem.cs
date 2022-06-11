using Core.Models;
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
        private readonly PlayerModel _playerModel;

        private EcsEntity _entity;
        private Rigidbody2D _rigidbody;    
              
        private bool OnGround => _entity.Has<OnGroundComponent>();

        internal PlayerJumpSystem(PlayerInputController playerInputController, PlayerModel playerModel) 
        {
            _playerInput = playerInputController;
            _playerModel = playerModel;
        }
        
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
            if (OnGround)
            {
                _rigidbody.AddForce(new Vector2(0, _playerModel.JumpForce), ForceMode2D.Impulse);
            }
        }   
    }
}
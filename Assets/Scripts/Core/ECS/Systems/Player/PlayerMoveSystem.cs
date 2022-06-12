using Core.Models;
using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<
            RigidbodyComponent, 
            SpriteRendererComponent,
            MovableComponent,
            PlayerTagComponent>.Exclude<DiedComponent> _filter = null;

        private readonly PlayerInputController _playerInput;  
       
        private Vector2 _moveDirection;

        internal PlayerMoveSystem(PlayerInputController playerInputController)
        {
            _playerInput = playerInputController;
        }
        
        public virtual void Init()
        {
            _playerInput.Keyboard.Move.performed += OnMove;                       
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Move.performed -= OnMove;
        }
        public void Run()
        {
            foreach (var i in _filter)
            {
                Rigidbody2D rigidbody = _filter.Get1(i).Value;
                SpriteRenderer spriteRenderer = _filter.Get2(i).Value;
                float speed = _filter.Get3(i).Value;

                // Set velocity
                Vector2 velocity = new Vector2(_moveDirection.x, 0) * speed * Time.deltaTime;

                // Move character
                rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);

                // Rotate character depending on his direction
                spriteRenderer.flipX = _moveDirection.x < 0;
            }
        }    
        
        private void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }               
    }
}
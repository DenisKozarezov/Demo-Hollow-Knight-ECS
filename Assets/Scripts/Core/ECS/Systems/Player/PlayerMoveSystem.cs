using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<PlayerMoveComponent> _filter = null;

        private const float MoveSpeed = 140f;

        private GameObject _gameObject;
        private readonly PlayerInputController _playerInput;       
       
        private Vector2 _moveDirection;
        private Vector2 _velocity;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        internal PlayerMoveSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Move.performed += OnMove;
            
            // Initialize references
            ref var ecsEntity = ref _filter.GetEntity(0);
            _gameObject = ecsEntity.Get<PlayerMoveComponent>().GameObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Move.performed -= OnMove;
        }
        public void Run()
        {
            // Set velocity
            _velocity = new Vector2(_moveDirection.x, 0) * MoveSpeed * Time.deltaTime;

            // Move character
            _rigidbody.velocity = new Vector2(_velocity.x, _rigidbody.velocity.y);

            // Rotate character depending on his direction
            _spriteRenderer.flipX = _moveDirection.x < 0;
        }    
        
        private void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }               
    }
}
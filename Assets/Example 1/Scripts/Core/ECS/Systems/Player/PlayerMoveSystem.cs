using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
    {        
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<PlayerMoveComponent> _filter = null;

        private const float MoveSpeed = 140f;

        private GameObject _gameObject;
        private PlayerInputController _playerInput;       
       
        private Vector2 _moveDirection;
        private Vector2 _velocity;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        
        public PlayerMoveSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Move.performed += OnMove;
            _playerInput.Enable();
            
            // Initialize references
            ref var ecsEntity = ref _filter.GetEntity(0);
            _gameObject = ecsEntity.Get<PlayerMoveComponent>().GameObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        }
        
        private void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }

        private void Move(Vector2 direction)
        {
            // Set velocity
            _velocity = new Vector2(direction.x, 0) * MoveSpeed * Time.deltaTime;

            // Move character
            _rigidbody.velocity = new Vector2(_velocity.x, _rigidbody.velocity.y);

            // Rotate character depending on his direction
            _spriteRenderer.flipX = direction.x < 0;
        }              

        public void Run()
        {
            Move(_moveDirection);
        }
    }
}
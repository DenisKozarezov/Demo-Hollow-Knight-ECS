using Examples.Example_1.ECS.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.ECS.Systems.Player
{
    public class PlayerJumpSystem : IEcsInitSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<PlayerMoveComponent> _filter = null;

        private GameObject _gameObject;
        private PlayerInputController _playerInput;
        
        private Transform _transform;
        private Rigidbody2D _body;

        private float jumpImpulse = 8;


        public PlayerJumpSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init() {
            _playerInput.Keyboard.Jump.performed += context => OnJump();
            _playerInput.Enable();
            
            ref var ecsEntity = ref _filter.GetEntity (0);
            _gameObject = ecsEntity.Get<PlayerJumpComponent>().GameObject;
            _body = _gameObject.GetComponent<Rigidbody2D>();
            _transform = _gameObject.transform;
            
        }

        private void OnJump() {
            if (Mathf.Abs(_body.velocity.y) < 0.1f)
                _body.AddForce(new Vector2(0, jumpImpulse), ForceMode2D.Impulse);
        }
    }
}
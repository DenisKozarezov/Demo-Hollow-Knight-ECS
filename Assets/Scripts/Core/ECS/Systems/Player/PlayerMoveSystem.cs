using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<
            RigidbodyComponent, 
            SpriteRendererComponent,
            MovableComponent,
            PlayerTagComponent>
            .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private Vector2 _lastDirection;

        public PlayerMoveSystem(IInputSystem playerInput)
        {
            _playerInput = playerInput;
        }

        private bool FlipSrite(Vector2 direction)
        {
            return direction.sqrMagnitude == 0f ? _lastDirection.x < 0f : direction.x < 0f;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                Rigidbody2D rigidbody = _filter.Get1(i).Value;
                SpriteRenderer spriteRenderer = _filter.Get2(i).Value;
                ref float speed = ref _filter.Get3(i).Value;

                // Set velocity
                Vector2 velocity = Vector2.right * _playerInput.Direction.x * speed * Time.deltaTime;

                // Move character
                rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);

                // Rotate character depending on his direction
                if (_playerInput.Direction.sqrMagnitude != 0f) _lastDirection = _playerInput.Direction;
                spriteRenderer.flipX = FlipSrite(_playerInput.Direction);
            }
        }                 
    }
}
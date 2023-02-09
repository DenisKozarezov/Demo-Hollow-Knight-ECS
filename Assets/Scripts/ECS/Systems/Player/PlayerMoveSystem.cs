using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerMoveSystem : IExecuteSystem
    {
        private readonly Services.ITimeService _time;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<InputEntity> _inputs;

        private Vector2 _lastDirection;

        public PlayerMoveSystem(GameContext game, InputContext input)
        {
            _time = game.time.Value;
            _heroes = game.GetGroup(GameMatcher.Player);
            _inputs = input.GetGroup(InputMatcher.Horizontal);
        }

        private bool FlipSrite(Vector2 direction)
        {
            return direction.sqrMagnitude == 0f ? _lastDirection.x < 0f : direction.x < 0f;
        }
        private void Move(GameEntity player, float direction, float speed)
        {
            Vector2 newPosition = player.rigidbody.Value.position + Vector2.right * direction * speed * _time.DeltaTime;
            player.rigidbody.Value.position = newPosition;

            //UpdateDirection(player, direction);
        }

        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                foreach (GameEntity hero in _heroes)
                {
                    float direction = input.horizontal.Value;
                    float speed = hero.movable.Value;

                    if (Mathf.Abs(direction) > 0)
                        Move(hero, Mathf.Sign(direction), speed);
                }
            }


            //foreach (var i in _filter)
            //{
            //    Rigidbody2D rigidbody = _filter.Get1(i).Value;
            //    SpriteRenderer spriteRenderer = _filter.Get2(i).Value;
            //    ref float speed = ref _filter.Get3(i).Value;

            //    // Set velocity
            //    Vector2 velocity = Vector2.right * _playerInput.Direction.x * speed * Time.fixedDeltaTime;

            //    // Move character
            //    rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);

            //    // Rotate character depending on his direction
            //    if (_playerInput.Direction.sqrMagnitude != 0f) _lastDirection = _playerInput.Direction;
            //    spriteRenderer.flipX = FlipSrite(_playerInput.Direction);
            //}
        }                 
    }
}
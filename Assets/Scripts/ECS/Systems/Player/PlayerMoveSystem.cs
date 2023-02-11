using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerMoveSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<InputEntity> _inputs;

        public PlayerMoveSystem(GameContext game, InputContext input)
        {
            _game = game;
            _players = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player)
                .NoneOf(GameMatcher.Dead));
            _inputs = input.GetGroup(InputMatcher.Horizontal);
        }

        private void Move(GameEntity player, float horizontal, float speed)
        {
            Vector2 newPosition = player.position.Value + Vector2.right * horizontal * speed * _game.time.Value.DeltaTime;
            newPosition.y = player.rigidbody.Value.position.y;
            player.ReplacePosition(newPosition);
        }
        private void UpdateDirection(GameEntity player, float horizontal)
        {
            bool alreadySynced = player.hasDirection && player.direction.Value == horizontal;

            if (alreadySynced) return;

            player.ReplaceDirection(horizontal);
        }
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                foreach (GameEntity player in _players)
                {
                    float direction = input.horizontal.Value;
                    float speed = player.movable.Value;

                    player.isMoving = Mathf.Abs(direction) > 0f;

                    if (player.isMoving)
                    {
                        int sign = System.Math.Sign(direction);
                        Move(player, horizontal: sign, speed);
                        UpdateDirection(player, horizontal: sign);
                    }
                }
            }
        }
    }
}
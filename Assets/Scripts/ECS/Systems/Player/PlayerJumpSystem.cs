using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerJumpSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _players;

        public PlayerJumpSystem(GameContext game, InputContext input) : base(input)
        {
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player, 
                    GameMatcher.Rigidbody, 
                    GameMatcher.Jump, 
                    GameMatcher.Grounded)
                .NoneOf(GameMatcher.Jumping, GameMatcher.Dead));
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.isJump;
        }
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Jump.Added());
        }
        protected override void Execute(List<InputEntity> entities)
        {
            foreach (InputEntity _ in entities)
            {
                foreach (GameEntity player in _players.GetEntities())
                {
                    player.isJumping = true;
                    player.AddDamageTaken(1);

                    float jumpHeight = player.jump.JumpForceRange.x;
                    float jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
                    player.rigidbody.Value.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }       
    }
}
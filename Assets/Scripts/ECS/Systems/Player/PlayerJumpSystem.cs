using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerJumpSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _heroes;

        public PlayerJumpSystem(GameContext game, InputContext input) : base(input)
        {
            _heroes = game.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.Player, 
                    GameMatcher.Rigidbody, 
                    GameMatcher.Jump, 
                    GameMatcher.OnGround).
                NoneOf(GameMatcher.Died));
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
                foreach (GameEntity hero in _heroes.GetEntities())
                {
                    float jumpHeight = hero.jump.JumpForceRange.x;
                    float jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
                    hero.rigidbody.Value.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }       
    }
}
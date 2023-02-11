using Entitas;

namespace Core.ECS.Systems.Units
{
    public sealed class FallingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public FallingSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Unit)
                .NoneOf(GameMatcher.Grounded, GameMatcher.Jumping, GameMatcher.Dead));
        }
        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities())
                entity.isJumping = true;
        }
    }
}

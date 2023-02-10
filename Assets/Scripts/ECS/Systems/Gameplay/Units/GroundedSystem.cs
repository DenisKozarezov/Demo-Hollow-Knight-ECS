using Entitas;

namespace Core.ECS.Systems.Units
{
    public sealed class GroundedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public GroundedSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.Grounded, GameMatcher.Jumping));
        }
        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities()) 
                entity.isJumping = false;
        }
    }
}

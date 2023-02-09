using Entitas;

namespace Core
{
    public sealed class GroundedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        public GroundedSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.AllOf(GameMatcher.Grounded, GameMatcher.Jumping));
        }
        public void Execute()
        {
            foreach (GameEntity hero in _heroes.GetEntities())
                hero.isJumping = false;
        }
    }
}

using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerInteractingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<InputEntity> _inputs;

        public PlayerInteractingSystem(GameContext game, InputContext input)
        {
            _players = game.GetGroup(GameMatcher
              .AllOf(GameMatcher.Player, GameMatcher.CanInteract)
              .NoneOf(GameMatcher.Dead));
            _inputs = input.GetGroup(InputMatcher.Look);
        }

        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                foreach (GameEntity player in _players)
                {
                    player.isInteracting = true;
                }
            }
        }
    }
}

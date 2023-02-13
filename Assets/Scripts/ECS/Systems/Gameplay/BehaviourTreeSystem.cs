using Entitas;

namespace Core.ECS.Systems
{
    public sealed class BehaviourTreeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _agents;
        public BehaviourTreeSystem(GameContext game)
        {
            _agents = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.BehaviourTree)
                .NoneOf(GameMatcher.Dead));
        }
        public void Execute()
        {
            foreach (GameEntity agent in _agents)
            {
                agent.ReplacePosition(agent.transform.Value.position);
                agent.behaviourTree.Value.Update();
            }
        }
    }
}
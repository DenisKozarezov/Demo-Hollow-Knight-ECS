using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Entitas;

namespace Core.AI.Agent.Conditions
{
    [Category("Agent/Conditions")]
    public class PlayerDead : Condition
    {
        private IGroup<GameEntity> _players;
        protected override void OnInit()
        {
            _players = (Agent as GameEntity).Context().GetGroup(GameMatcher
              .AllOf(GameMatcher.Player)
              .NoneOf(GameMatcher.Dead));
        }
        protected override bool Check() => _players.count == 0;
    }
}
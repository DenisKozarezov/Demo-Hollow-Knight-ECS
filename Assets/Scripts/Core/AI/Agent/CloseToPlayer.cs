using UnityEngine;
using Entitas;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;

namespace Core.AI.Agent.Conditions
{
    [Category("Agent/Conditions")]
    public class CloseToPlayer : Condition
    {
        [SerializeField, Min(0f)]
        private float _distance;
        private IGroup<GameEntity> _players;

        protected override void OnInit()
        {
            _players = (Agent as GameEntity).Context().GetGroup(GameMatcher
               .AllOf(GameMatcher.Player)
               .NoneOf(GameMatcher.Dead));
        }
        protected override bool Check()
        {
            if (_players.count == 0) return false;

            Vector2 playerPos = _players.GetEntities()[0].position.Value;
            Vector2 agentPos = (Agent as GameEntity).position.Value;
            return (playerPos - agentPos).sqrMagnitude < _distance * _distance;
        }
    }
}
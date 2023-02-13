using BehaviourTree.Runtime.Nodes;
using Entitas;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class LookAtPlayer : Action
    {
        private GameEntity _entity;
        private IGroup<GameEntity> _players;

        protected override void OnInit()
        {
            _entity = (GameEntity)Agent;
            _players = _entity.Context().GetGroup(GameMatcher
                .AllOf(GameMatcher.Player)
                .NoneOf(GameMatcher.Dead));
        }
        protected override State OnUpdate()
        {
            if (_players.count == 0) return State.Failure;
            else
            {
                foreach (GameEntity player in _players)
                {
                    float playerX = player.position.Value.x;
                    float sign = UnityEngine.Mathf.Sign(playerX - _entity.position.Value.x);
                    _entity.ReplaceDirection(sign);
                }
                return State.Success;
            }
        }
    }
}
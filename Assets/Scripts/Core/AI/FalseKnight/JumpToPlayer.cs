using UnityEngine;
using Entitas;
using BehaviourTree.Runtime.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class JumpToPlayer : Action
    {
        private GameEntity _entity;
        private IGroup<GameEntity> _players;
        private float _jumpForce;

        protected override void OnInit()
        {
            _entity = (GameEntity)Agent;
            _players = _entity.Context().GetGroup(GameMatcher
                .AllOf(GameMatcher.Player)
                .NoneOf(GameMatcher.Dead));

            float jumpForceY = _entity.jump.JumpForceRange.y;
            _jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpForceY);
        }
        protected override State OnUpdate()
        {
            if (_players.count == 0) return State.Failure;
            else
            {
                foreach (GameEntity player in _players)
                {
                    // Calculate distance
                    float playerX = player.position.Value.x;
                    float distanceToPlayer = playerX - _entity.position.Value.x;

                    // Jump
                    Vector2 velocity = new Vector2(distanceToPlayer * 0.8f, _jumpForce);
                    _entity.rigidbody.Value.velocity = velocity;
                    _entity.isJumping = true;
                }
                return State.Success;
            }    
        }
    }
}
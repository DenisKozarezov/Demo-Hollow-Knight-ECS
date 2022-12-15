using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class JumpToPlayer : Action
    {
        private EcsFilter _filter;
        private EcsEntity _player;
        private float _jumpForce;
        private Rigidbody2D _rigidbody;

        protected override void OnInit()
        {
            float jumpHeight = Agent.Get<JumpComponent>().JumpForceRange.y;
            _jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
            _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>.Exclude<DiedComponent>));
            _rigidbody = Agent.Get<RigidbodyComponent>().Value;
        }
        protected override void OnStart()
        {
            _player = _filter.GetEntity(0);
        }      
        protected override State OnUpdate()
        {
            if (_rigidbody == null || _player.IsNullOrEmpty()) return State.Failure;

            // Calculate distance
            float playerX = _player.Get<SpriteRendererComponent>().Value.transform.position.x;
            float distanceToPlayer = (playerX - _rigidbody.transform.position.x) * 0.8f;

            // Too close to player
            if (Mathf.Abs(distanceToPlayer) < 3f) return State.Failure;

            // Jump
            Vector2 velocity = new Vector2(distanceToPlayer, _jumpForce);
            _rigidbody.velocity = velocity;
            return State.Success;
        }
    }
}
using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using AI.BehaviourTree.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    public class JumpRight : ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private float _jumpForce;
        private bool OnGround => BehaviorTreeRef.Agent.Has<OnGroundComponent>();

        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.Agent.Get<SpriteRendererComponent>().Value;
            _rigidbody = BehaviorTreeRef.Agent.Get<RigidbodyComponent>().Value;
            _jumpForce = BehaviorTreeRef.Agent.Get<JumpComponent>().JumpForceRange.x;
        }
        protected override void OnStart()
        {
            _spriteRenderer.flipX = false;
        }
        protected override State OnUpdate()
        {
            if (!BehaviorTreeRef.Agent.IsNullOrEmpty()) return State.Failure;

            if (OnGround)
            {
                _rigidbody.velocity = new Vector2(-3, _jumpForce);
                return State.Success;
            }
            return State.Failure;
        }
    }
}
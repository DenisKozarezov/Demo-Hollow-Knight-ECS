using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviourTree.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    public class JumpLeft : ActionNode
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
            _spriteRenderer.flipX = true;
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
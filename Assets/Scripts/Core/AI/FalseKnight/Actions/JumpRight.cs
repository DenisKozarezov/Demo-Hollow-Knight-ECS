using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class JumpRight : ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private float _jumpForce;

        private bool OnGround => BehaviorTreeRef.EntityReference.Entity.Has<OnGroundComponent>();

        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
            _rigidbody = BehaviorTreeRef.EntityReference.Entity.Get<RigidbodyComponent>().Value;
            _jumpForce = BehaviorTreeRef.EntityReference.Entity.Get<JumpComponent>().Value;
        }
        protected override State OnUpdate()
        {
            if (!BehaviorTreeRef.EntityReference) return State.Failure;

            bool lookRight = !_spriteRenderer.flipX;

            if (OnGround)
            {
                if (!lookRight) _spriteRenderer.flipX = false;

                _rigidbody.velocity = new Vector2(-3, _jumpForce);
                return State.Success;
            }
            return State.Failure;
        }
    }
}
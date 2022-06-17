using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    public class JumpLeft : ActionNode
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

            bool lookLeft = _spriteRenderer.flipX;

            if (OnGround)
            {
                if (!lookLeft) _spriteRenderer.flipX = true;

                _rigidbody.velocity = new Vector2(-3, _jumpForce);                
                return State.Success;
            }
            return State.Failure;
        }
    }
}
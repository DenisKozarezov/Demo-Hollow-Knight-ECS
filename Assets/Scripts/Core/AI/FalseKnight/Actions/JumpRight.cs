using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;
using Examples.Example_1.ECS.ComponentProviders;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class JumpRight : ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private bool OnGround => BehaviorTreeRef.EntityReference.Entity.Has<OnGroundComponent>();

        protected override void OnStart()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
            _rigidbody = BehaviorTreeRef.EntityReference.Entity.Get<RigidbodyComponent>().Value;
            _animator = BehaviorTreeRef.EntityReference.Entity.Get<AnimatorComponent>().Value;
        }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {
            if (!BehaviorTreeRef.EntityReference) return State.Failure;

            bool lookRight = !_spriteRenderer.flipX;

            if (OnGround)
            {
                if (!lookRight) _spriteRenderer.flipX = false;

                _rigidbody.velocity = new Vector2(-3, 10);
                _animator.SetTrigger("Jump");

                return State.Success;
            }
            return State.Failure;
        }
    }
}
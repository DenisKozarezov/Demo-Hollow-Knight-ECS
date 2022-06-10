using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;
using Examples.Example_1.ECS.ComponentProviders;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class JumpLeft : ActionNode
    {
        private EntityReference _entityReference;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private bool OnGround => _entityReference.Entity.Has<OnGroundComponent>();

        protected override void OnStart()
        {
            _entityReference = BehaviorTreeRef.GameObjectRef.GetComponent<EntityReference>();
            _spriteRenderer = _entityReference.GetComponent<SpriteRenderer>();
            _rigidbody = _entityReference.GetComponent<Rigidbody2D>();
            _animator = _entityReference.GetComponent<Animator>();
        }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {
            if (!BehaviorTreeRef.GameObjectRef || !_entityReference) return State.Failure;

            bool lookLeft = _spriteRenderer.flipX;

            if (OnGround)
            {
                if (!lookLeft) _spriteRenderer.flipX = true;

                _rigidbody.velocity = new Vector2(-3, 10);
                _animator.SetTrigger("Jump");
                
                return State.Success;
            }
            return State.Failure;
        }
    }
}
using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    //public class JumpLeft : ActionNode
    //{
    //    private SpriteRenderer _spriteRenderer;
    //    private Rigidbody2D _rigidbody;
    //    private float _jumpForce;

    //    private bool OnGround => Agent.Has<OnGroundComponent>();

    //    protected override void OnInit()
    //    {
    //        _spriteRenderer = Agent.Get<SpriteRendererComponent>().Value;
    //        _rigidbody = Agent.Get<RigidbodyComponent>().Value;
    //        _jumpForce = Agent.Get<JumpComponent>().JumpForceRange.x;
    //    }
    //    protected override void OnStart()
    //    {
    //        _spriteRenderer.flipX = true;
    //    }
    //    protected override State OnUpdate()
    //    {
    //        if (OnGround)
    //        {
    //            _rigidbody.velocity = new Vector2(-3, _jumpForce);                
    //            return State.Success;
    //        }
    //        return State.Failure;
    //    }
    //}
}
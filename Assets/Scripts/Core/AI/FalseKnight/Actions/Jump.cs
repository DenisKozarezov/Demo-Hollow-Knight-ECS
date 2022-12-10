using System;
using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using Core.ECS.Components.Units;
using Core.AI.FalseKnight.Parameters;
using Core.Units;

namespace Core.AI.FalseKnight.Actions
{
    public class Jump : ActionNode
    {
        private Fatigue _fatigue;
        private Rigidbody2D _rigidbody;
        private float _jumpForce;
        private Transform _player;

        protected override void OnInit()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
            _rigidbody = BehaviorTreeRef.EntityReference.Entity.Get<RigidbodyComponent>().Value;
            float jumpHeight = BehaviorTreeRef.EntityReference.Entity.Get<JumpComponent>().JumpForceRange.y;
            _jumpForce = Utils.CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);
            _player = FindObjectsOfType<UnitView>().Where(i => i.gameObject.layer == Constants.PlayerLayer).First().transform;
        }
        private bool PlayerIsLeft() => (_rigidbody.transform.position.x - _player.position.x ) > 0;
        
        protected override State OnUpdate()
        {           
            if (BehaviorTreeRef == null) return State.Failure;

            float distance = Math.Abs(_rigidbody.transform.position.x - _player.position.x);
            float distanceToPlayer = distance > 1 ? 1 : distance;
            Vector2 jumpForce = new Vector2(_jumpForce * distanceToPlayer, _jumpForce);

            if (PlayerIsLeft()) jumpForce.x *= -1;

            _rigidbody.velocity = jumpForce;
            _fatigue.Value += 0.6f;
            return State.Success;
        }
        
        public override float Cost(ParameterNode parameter)
        {
            DistanceToPlayer distanceToPlayer = parameter as DistanceToPlayer;
            if (distanceToPlayer)
            {
                return distanceToPlayer.Value > 5f ? 1f : 0f;
            }
    
            Fatigue fatigue = parameter as Fatigue;
            if (fatigue)
            {
                return fatigue.Value == 0f || fatigue.Value < 0.01f ? 1f : 0f;
            }
            
            Grounded grounded = parameter as Grounded;
            if (grounded)
            {
                return grounded.Cost();
            }
            
            return base.Cost();
        }
    }
}
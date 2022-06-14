using System.Linq;
using UnityEngine;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.FalseKnight.AI.Parameters;
using Examples.Example_1.ECS;
using Leopotam.Ecs;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class Jump : ActionNode
    {
        private Fatigue _fatigue;
        private Rigidbody2D _rigidbody;
        private float _jumpForce;

        protected override void OnInit()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
            _rigidbody = BehaviorTreeRef.EntityReference.Entity.Get<RigidbodyComponent>().Value;
            _jumpForce = BehaviorTreeRef.EntityReference.Entity.Get<JumpComponent>().Value;
        }
        protected override State OnUpdate()
        {           
            if (BehaviorTreeRef == null) return State.Failure;

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            if (_fatigue) _fatigue.Value += 1.4f;
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
                return grounded.Value ? 1f : 0f;
            }
            
            return 1f;
        }
    }
}
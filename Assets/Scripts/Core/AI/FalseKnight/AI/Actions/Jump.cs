using System.Linq;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.FalseKnight.AI.Parameters;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class Jump : ActionNode
    {
        private Fatigue _fatigue;
        private Rigidbody2D _rigidbody;

        protected override void OnStart()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
            _rigidbody = BehaviorTreeRef.GameObjectRef.GetComponent<Rigidbody2D>();
        }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {           
            if (BehaviorTreeRef == null) return State.Failure;

            _rigidbody.velocity = new Vector2(0, 10);
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
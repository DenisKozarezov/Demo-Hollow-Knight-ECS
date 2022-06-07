using System.Linq;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.FalseKnight.AI.Parameters;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class Jump : ActionNode
    {
        private Fatigue _fatigue;

        public override void OnStart()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
        }
        public override void OnStop()
        {
          
        }
        public override State OnUpdate()
        {           
            if (this.BehaviorTreeRef == null)
                return State.Failure;
   
            this.BehaviorTreeRef.GameObjectRef.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            if (_fatigue)
                _fatigue.Value += 1.4f;
            return State.Success;
        }
        
        public override float Cost(ParameterNode parametr)
        {
            DistanceToPlayer distanceToPlayer = parametr as DistanceToPlayer;
            if (distanceToPlayer)
            {
                if (distanceToPlayer.Value > 5)
                    return 1;
                return 0;
            }
    
            Fatigue fatigue = parametr as Fatigue;
            if (fatigue)
            {
                if (fatigue.Value == 0 || fatigue.Value < 0.01f)
                    return 1;
                return 0;
            }
            
            Grounded grounded = parametr as Grounded;
            if (grounded)
            {
                if (grounded.Value == true)
                    return 1;
                return 0;
            }
            
            return 1;
        }
    }
}
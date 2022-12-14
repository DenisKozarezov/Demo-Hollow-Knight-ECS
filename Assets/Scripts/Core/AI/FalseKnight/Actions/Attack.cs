using System.Linq;
using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using Core.ECS.Events.FalseKnight;
using Core.AI.FalseKnight.Parameters;

namespace Core.AI.FalseKnight.Actions
{
    public class Attack : ActionNode
    {
        private Fatigue _fatigue;

        protected override void OnInit()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
        }
        protected override State OnUpdate()
        {
            BehaviorTreeRef.Agent.Get<FalseKnightAttackEventComponent>();
            _fatigue.Value += 0.4f;
            return State.Success;
        }

        public override float Cost(ParameterNode parameter)
        {
            DistanceToPlayer distanceToPlayer = parameter as DistanceToPlayer;
            if (distanceToPlayer)
            {
                return distanceToPlayer.Value < 5f ? 1f : 0f;
            }
    
            Fatigue fatigue = parameter as Fatigue;
            if (fatigue)
            {
                return fatigue.Value < 0.2f ? 1f : 0f;
            }
            
            return base.Cost();
        }
    }
}
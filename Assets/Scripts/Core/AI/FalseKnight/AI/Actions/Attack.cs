using System.Linq;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS.Events.FalseKnight;
using Examples.Example_1.FalseKnight.AI.Parameters;
using Leopotam.Ecs;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class Attack : ActionNode
    {
        private Fatigue _fatigue;

        public override void OnStart()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
        }
        public override void OnStop() { }
        public override State OnUpdate()
        {
            EcsEntity attackAnimationEntity = _world.NewEntity();
            attackAnimationEntity.Get<FalseKnightAttackEventComponent>().GameObjectRef = BehaviorTreeRef.GameObjectRef;
            if (_fatigue)
                _fatigue.Value += 1;
            return State.Success;
        }

        public override float Cost(ParameterNode parametr)
        {
            DistanceToPlayer distanceToPlayer = parametr as DistanceToPlayer;
            if (distanceToPlayer)
            {
                return distanceToPlayer.Value < 5f ? 1f : 0f;
            }
    
            Fatigue fatigue = parametr as Fatigue;
            if (fatigue)
            {
                return fatigue.Value < 0.2f ? 1f : 0f;
            }
            
            return 1f;
        }
    }
}
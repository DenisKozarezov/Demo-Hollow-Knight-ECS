using System.Linq;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS.Events.FalseKnight;
using Examples.Example_1.ECS.Systems.FalseKnight;
using Examples.Example_1.FalseKnight.AI.Parameters;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class Attack : ActionNode
    {
        private Fatigue _fatigue;
        public override void OnInit()
        {
            base.OnInit();
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
        }
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
                if (distanceToPlayer.Value < 5)
                    return 1;
                return 0;
            }
    
            Fatigue fatigue = parametr as Fatigue;
            if (fatigue)
            {
                if (fatigue.Value < 0.2f)
                    return 1;
                return 0;
            }
            
            return 1;
        }
    }
}
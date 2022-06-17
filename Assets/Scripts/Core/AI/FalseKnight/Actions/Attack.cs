using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using Core.ECS.Components.Units;
using Core.ECS.Events.FalseKnight;
using Core.AI.FalseKnight.Parameters;

namespace Core.AI.FalseKnight.Actions
{
    public class Attack : ActionNode
    {
        private Fatigue _fatigue;
        private Animator _animator;

        protected override void OnInit()
        {
            _fatigue = BehaviorTreeRef.Nodes.Where(n=> n is Fatigue).FirstOrDefault() as Fatigue;
            _animator = BehaviorTreeRef.EntityReference.Entity.Get<AnimatorComponent>().Value;
        }
        protected override State OnUpdate()
        {
            EcsEntity attackAnimationEntity = _world.NewEntity();
            attackAnimationEntity.Get<FalseKnightAttackEventComponent>().Animator = _animator;
            if (_fatigue) _fatigue.Value += 1;
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
            
            return 1f;
        }
    }
}
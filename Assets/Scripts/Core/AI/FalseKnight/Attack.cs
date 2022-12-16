using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Events.FalseKnight;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class Attack : Action
    {
        protected override State OnUpdate()
        {
            Agent.Get<FalseKnightAttackEventComponent>();
            return State.Success;
        }
    }
}
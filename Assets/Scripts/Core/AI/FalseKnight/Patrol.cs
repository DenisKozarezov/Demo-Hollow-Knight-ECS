using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Events.FalseKnight;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class Patrol : Action
    {
        protected override State OnUpdate()
        {  
            return State.Success;
        }
    }
}
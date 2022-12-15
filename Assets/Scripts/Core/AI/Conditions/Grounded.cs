using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Core.ECS.Components.Units;

namespace Core.AI.Units.Conditions
{
    [Category("Units/Conditions")]
    public class Grounded : Condition
    {
        protected override bool Check() => Agent.Has<OnGroundComponent>();
    }
}
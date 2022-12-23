using Leopotam.Ecs;
using Core.ECS.Components.Units;
using BehaviourTree.Runtime.Nodes;

namespace Core.AI.Agent.Actions
{
    [Category("Agent/Actions")]
    public class Kill : Action
    {
        protected override State OnUpdate()
        {
            Agent.Get<HealthComponent>().Health = 0;
            return State.Success;
        }
    }
}
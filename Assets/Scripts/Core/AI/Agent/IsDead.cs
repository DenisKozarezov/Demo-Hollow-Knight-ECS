using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;

namespace Core.AI.Agent.Conditions
{
    [Category("Agent/Conditions")]
    public class IsDead : Condition
    {
        protected override bool Check() => (Agent as GameEntity).isDead;
    }
}
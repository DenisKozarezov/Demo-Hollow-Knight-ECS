using BehaviourTree.Runtime.Nodes;

namespace Core.AI.Agent.Actions
{
    [Category("Agent/Actions")]
    public class Kill : Action
    {
        protected override State OnUpdate()
        {
            (Agent as GameEntity).ReplaceCurrentHp(0);
            return State.Success;
        }
    }
}
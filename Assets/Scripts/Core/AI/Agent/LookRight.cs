using BehaviourTree.Runtime.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    [Category("Agent/Actions")]
    public class LookRight : Action
    {
        protected override State OnUpdate()
        {
            (Agent as GameEntity).ReplaceDirection(1f);
            return State.Success;
        }
    }
}
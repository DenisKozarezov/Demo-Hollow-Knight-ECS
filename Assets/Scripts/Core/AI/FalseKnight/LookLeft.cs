using BehaviourTree.Runtime.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class LookLeft : Action
    {
        protected override State OnUpdate()
        {
            (Agent as GameEntity).ReplaceDirection(-1f);
            return State.Success;
        }
    }
}
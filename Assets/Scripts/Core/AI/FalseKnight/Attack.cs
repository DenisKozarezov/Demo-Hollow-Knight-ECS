using BehaviourTree.Runtime.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class Attack : Action
    {
        protected override State OnUpdate()
        {
            (Agent as GameEntity).AddAttacking(ECS.Components.Units.AttackDirection.Default);
            return State.Success;
        }
    }
}
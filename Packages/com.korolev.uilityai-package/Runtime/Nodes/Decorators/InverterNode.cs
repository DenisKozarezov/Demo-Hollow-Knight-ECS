namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class InverterNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            return Child.State == State.Success ? State.Failure : State.Success;
        }
    }
}
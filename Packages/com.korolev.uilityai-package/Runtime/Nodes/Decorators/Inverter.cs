namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Inverter : Decorator
    {
        protected override State OnUpdate()
        {
            return Child.State == State.Success ? State.Failure : State.Success;
        }
    }
}
namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Inverter : Decorator
    {
        protected override State OnUpdate()
        {
            return Child.Update() == State.Success ? State.Failure : State.Success;
        }
    }
}
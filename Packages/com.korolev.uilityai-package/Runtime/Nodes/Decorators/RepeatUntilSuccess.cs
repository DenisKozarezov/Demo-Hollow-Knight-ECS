namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class RepeatUntilSuccess : Decorator
    {
        protected override State OnUpdate()
        {
            if (Child.Update() == State.Success) return State.Success;

            return State.Running;
        }
    }
}
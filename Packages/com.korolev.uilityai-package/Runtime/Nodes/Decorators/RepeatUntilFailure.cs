namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class RepeatUntilFailure : Decorator
    {
        protected override State OnUpdate()
        {
            if (Child.Update() == State.Failure) return State.Failure;

            return State.Running;
        }
    }
}
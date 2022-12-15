namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public abstract class Condition : Decorator
    {
        protected override State OnUpdate()
        {
            if (Child == null) return State.Failure;
            return Check() ? State.Success : State.Failure;         
        }
        protected abstract bool Check();
    }
}
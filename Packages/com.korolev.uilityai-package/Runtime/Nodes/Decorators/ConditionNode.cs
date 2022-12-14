namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public abstract class ConditionNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            if (Child == null) return State.Failure;
            return Check() ? State.Success : State.Failure;         
        }
        protected abstract bool Check();
    }
}
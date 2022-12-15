namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Success : Decorator
    {
        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}
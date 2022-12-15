namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Failure : Decorator
    {
        protected override State OnUpdate()
        {
            return State.Failure;
        }
    }
}
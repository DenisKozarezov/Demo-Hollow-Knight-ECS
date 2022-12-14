namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class FailureNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            return State.Failure;
        }
    }
}
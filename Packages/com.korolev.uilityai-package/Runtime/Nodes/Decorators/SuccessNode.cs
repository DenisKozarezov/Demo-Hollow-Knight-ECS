namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class SuccessNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}
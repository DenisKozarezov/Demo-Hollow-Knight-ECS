namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class RootNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            if (Child == null) return State.Failure;

            return Child.Update();
        }
    }
}
namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class RepeatNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            Child.Update();
            return State.Running;
        }
    }
}
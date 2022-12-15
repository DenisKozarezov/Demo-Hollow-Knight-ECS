namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Repeat : Decorator
    {
        protected override State OnUpdate()
        {
            Child.Update();
            return State.Running;
        }
    }
}
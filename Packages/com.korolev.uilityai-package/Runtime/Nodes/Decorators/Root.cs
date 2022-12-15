namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public class Root : Decorator
    {
        protected override State OnUpdate()
        {
            if (Child == null) return State.Failure;

            return Child.Update();
        }
    }
}
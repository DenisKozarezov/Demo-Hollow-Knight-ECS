namespace BehaviourTree.Runtime.Nodes.Decorators
{
    public abstract class Condition : Decorator
    {
        protected override State OnUpdate()
        {
            bool check = Check();

            if (Child == null) return check ? State.Success : State.Failure;

            return check ? Child.Update() : State.Failure;
        }
        protected abstract bool Check();
    }
}
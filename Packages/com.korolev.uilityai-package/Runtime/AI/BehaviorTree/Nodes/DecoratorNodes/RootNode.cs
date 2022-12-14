namespace AI.BehaviourTree.Nodes.Decorators
{
    public class RootNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            if (Child.State != State.Running) return Child.State;
            
            BehaviourTreeRef.SetCurrentNode(Child);
            
            return State.Running;
        }
    }
}
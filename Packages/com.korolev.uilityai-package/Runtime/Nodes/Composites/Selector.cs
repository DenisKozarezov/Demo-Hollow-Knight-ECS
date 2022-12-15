namespace BehaviourTree.Runtime.Nodes.Composites
{
    public class Selector : CompositeNode
    {
        protected override State OnUpdate()
        {
            for (int i = _currentIndex; i < Children.Count; i++)
            {
                Node child = Children[_currentIndex];
                State state = child.Update();
                if (state != State.Failure) return state;
                _currentIndex++;
            }
            return State.Failure;
        }
    }   
}
namespace BehaviourTree.Runtime.Nodes.Composites
{
    public class ParallelFor : Composite
    {
        private int _successCount;
        protected override void OnStart()
        {
            _successCount = 0;
        }
        protected override State OnUpdate()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                switch (Children[i].Update())
                {
                    case State.Failure: return State.Failure;
                    case State.Success: _successCount++; break;
                }
                _currentIndex = i;
            }
            return _successCount == Children.Count ? State.Success : State.Running;
        }
    }   
}
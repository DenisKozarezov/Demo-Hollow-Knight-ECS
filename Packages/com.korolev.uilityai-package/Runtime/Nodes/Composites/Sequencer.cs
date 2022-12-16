namespace BehaviourTree.Runtime.Nodes.Composites
{
    public class Sequencer : Composite
    {
        protected override State OnUpdate() 
        {
            switch (Children[_currentIndex].Update())
            {
                case State.Success:
                    _currentIndex++;
                    break;
                case State.Running: return State.Running;
                case State.Failure: return State.Failure;
            }
            return AllChildrenEnumerated ? State.Success : State.Running;
        }
    }
}
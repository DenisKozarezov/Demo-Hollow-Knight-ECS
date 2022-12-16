namespace BehaviourTree.Runtime.Nodes.Composites
{
    public class Selector : Composite
    {
        protected override State OnUpdate()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                State state = Children[i].Update();
                switch (state)
                {
                    case State.Failure: continue;
                    default: return state;
                }
            }
            return State.Failure;
        }
    }   
}
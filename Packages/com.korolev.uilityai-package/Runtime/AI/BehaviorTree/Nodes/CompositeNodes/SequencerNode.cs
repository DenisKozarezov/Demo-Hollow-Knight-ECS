/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

namespace AI.BehaviourTree.Nodes.Composites
{
    public class SequencerNode : CompositeNode
    {
        private int _currentIndex = 0;
        protected override void OnStart() 
        { 
            _currentIndex = 0;
        }
        protected override State OnUpdate() 
        {
            if (ChildNodes.Count > 0)
            {
                if (_currentIndex >= ChildNodes.Count)
                    return State.Success;
                
                if (ChildNodes[_currentIndex].State == State.Failure)
                    return State.Failure;

                BehaviorTreeRef.SetCurrentNode(ChildNodes[_currentIndex]);
                _currentIndex++;
                return State.Running;
            }
            return State.Success;
        }
    }
}
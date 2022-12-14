/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

namespace AI.BehaviourTree.Nodes.Decorators
{
    public class RootNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            if (Child.State != State.Running) return Child.State;
            
            BehaviorTreeRef.SetCurrentNode(Child);
            
            return State.Running;
        }
    }
}
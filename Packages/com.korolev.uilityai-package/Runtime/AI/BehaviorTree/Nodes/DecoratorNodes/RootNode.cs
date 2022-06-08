/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

namespace AI.BehaviorTree.Nodes.DecoratorNodes
{
    public class RootNode : DecoratorNode
    {
        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {
            if (Child.State != State.Running)
                return Child.State;
            
            BehaviorTreeRef.SetCurrentNode(Child);
            
            return State.Running;
        }
    }
}
/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;

namespace AI.BehaviorTree.Nodes.DecoratorNodes
{
    public class RepeatNode : DecoratorNode
    {
        [HideInInspector] public ConditionNode ConditionNode;

        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {
            //если заданное условие выполняется
            if (ConditionNode == null || ConditionNode.Condition())
            {
                BehaviorTreeRef.SetCurrentNode(Child);
                return State.Running; 
            }
            return State.Success;
        }
    }
}
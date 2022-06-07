/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;

namespace AI.BehaviorTree.Nodes.ActionNodes
{
    public class DebugLogNode : ActionNode
    {
        public string Message;

        public override void OnStart() { }
        public override void OnStop() { }
        public override State OnUpdate() 
        {
            Debug.Log($"DebugLogNode: '{Message}'");
            return State.Success;
        }
    }
}
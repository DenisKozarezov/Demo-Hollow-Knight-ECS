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

        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override State OnUpdate() 
        {
            Debug.Log($"DebugLogNode: '{Message}'");
            return State.Success;
        }
        public override Node Clone()
        {
            DebugLogNode clone = Instantiate(this);
            clone.Parent = null;
            clone.Message = clone.Message.Clone().ToString();
            clone.GUID = GUID;
            return clone;
        }
    }
}
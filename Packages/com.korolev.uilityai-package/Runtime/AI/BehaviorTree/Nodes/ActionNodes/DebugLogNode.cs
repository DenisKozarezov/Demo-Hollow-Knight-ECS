using UnityEngine;

namespace AI.BehaviourTree.Nodes.Actions
{
    public class DebugLogNode : ActionNode
    {
        public string Message;            

        protected override State OnUpdate() 
        {
#if UNITY_EDITOR
            Debug.Log($"DebugLogNode: '{Message}'");
#endif
            return State.Success;
        }
        public override Node Clone()
        {
            DebugLogNode clone = base.Clone() as DebugLogNode;
            clone.Message = Message.Clone().ToString();
            return clone;
        }
    }
}
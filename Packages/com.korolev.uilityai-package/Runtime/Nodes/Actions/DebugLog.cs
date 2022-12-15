using UnityEngine;

namespace BehaviourTree.Runtime.Nodes.Actions
{
    public class DebugLog : Action
    {
        [SerializeField]
        private string _message;

        protected override State OnUpdate() 
        {
#if UNITY_EDITOR
            Debug.Log(_message);
#endif
            return State.Success;
        }
        public override Node Clone()
        {
            DebugLog clone = base.Clone() as DebugLog;
            clone._message = _message.Clone().ToString();
            return clone;
        }
    }
}
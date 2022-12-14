using UnityEngine;

namespace BehaviourTree.Runtime.Nodes.Actions
{
    public class WaitForSecondsNode : ActionNode
    {
        [SerializeField, Min(0f)]
        private float _duration;
        private float _startTime;

        protected override void OnStart()
        {
            _startTime = Time.time;
        }
        protected override State OnUpdate()
        {
            if (Time.time - _startTime > _duration)
            {
                return State.Success;
            }
            return State.Running;
        }       
    }
}
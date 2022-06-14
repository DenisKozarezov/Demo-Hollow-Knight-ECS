/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;

namespace AI.BehaviorTree.Nodes.ActionNodes
{
    public class WaitNode : ActionNode
    {
        [HideInInspector] private float _currentTimeWait = 0;
        public float TimeWait;

        protected override State OnUpdate()
        {
            if (_currentTimeWait >= TimeWait)
            {
                _currentTimeWait = 0;
                return State.Success;
            }
            _currentTimeWait += Time.deltaTime;
            return State.Running;
        }
        
        public override Node Clone()
        {
            WaitNode clone = Instantiate(this);
            clone.Parent = null;
            clone.TimeWait = TimeWait;
            clone.GUID = GUID;
            return clone;
        }
    }
}
using System;

namespace AI.BehaviorTree.Nodes.ParameterNodes
{
    public class BooleanNode: ParameterNode
    {
        [NonSerialized] public bool Value;

        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override State OnUpdate() 
        {
            return State.Success;
        }
    }
}
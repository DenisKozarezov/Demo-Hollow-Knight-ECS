using System;

namespace AI.BehaviorTree.Nodes.ParameterNodes
{
    public class BooleanNode: ParameterNode
    {
        [NonSerialized] public bool Value;

        public override void OnStart() { }
        public override void OnStop() { }
        public override State OnUpdate() 
        {
            return State.Success;
        }
    }
}
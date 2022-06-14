using System;

namespace AI.BehaviorTree.Nodes.ParameterNodes
{
    public class BooleanNode: ParameterNode
    {
        [NonSerialized] public bool Value;

        protected override State OnUpdate() 
        {
            return State.Success;
        }
        
        public override Node Clone()
        {
            BooleanNode clone = Instantiate(this);
            clone.ChildNode = null;
            clone.Value = Value;
            clone.GUID = GUID;
            return clone;
        }
    }
}
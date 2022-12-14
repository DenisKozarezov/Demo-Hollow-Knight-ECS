using System;

namespace AI.BehaviourTree.Nodes.Parameters
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
            BooleanNode clone = base.Clone() as BooleanNode;
            clone.Value = Value;
            return clone;
        }
    }
}
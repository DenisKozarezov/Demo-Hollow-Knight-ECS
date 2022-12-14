namespace AI.BehaviourTree.Nodes.Parameters
{
    public class FloatNode : ParameterNode
    { 
        public float Value = 0f;
        protected override State OnUpdate() 
        {
            return State.Success;
        }        
        public override Node Clone()
        {
            FloatNode clone = base.Clone() as FloatNode;
            clone.Value = Value;
            return clone;
        }
    }
}
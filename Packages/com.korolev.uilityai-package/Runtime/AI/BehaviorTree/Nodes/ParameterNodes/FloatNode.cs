/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

namespace AI.BehaviorTree.Nodes.ParameterNodes
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
            FloatNode clone = Instantiate(this);
            clone.ChildNode = null;
            clone.Value = Value;
            clone.GUID = GUID;
            return clone;
        }
    }
}
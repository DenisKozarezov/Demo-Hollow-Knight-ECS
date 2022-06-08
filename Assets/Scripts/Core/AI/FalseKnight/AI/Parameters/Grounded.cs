using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class Grounded : BooleanNode
    {
        private FalseKnight _falseKnight;
        protected override void OnStart()
        {
            _falseKnight = BehaviorTreeRef.GameObjectRef.GetComponent<FalseKnight>();         
        }
        protected override State OnUpdate()
        {
            Value = _falseKnight.Grounded;
            return State.Success;
        }
    }
}
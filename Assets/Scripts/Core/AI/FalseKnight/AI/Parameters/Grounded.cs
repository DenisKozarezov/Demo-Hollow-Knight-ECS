using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class Grounded : BooleanNode
    {
        private FalseKnight _falseKnight;
        public override void OnStart()
        {
            _falseKnight = BehaviorTreeRef.GameObjectRef.GetComponent<FalseKnight>();         
        }
        public override void OnStop() { }
        public override State OnUpdate()
        {
            Value = _falseKnight.Grounded;
            return State.Success;
        }
    }
}
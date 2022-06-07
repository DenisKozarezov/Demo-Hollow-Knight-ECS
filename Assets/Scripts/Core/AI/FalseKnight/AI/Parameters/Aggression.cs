using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class Aggression : FloatNode
    {
        public override void OnInit() {}
        public override void OnStart() {  State = State.Running; }
        public override void OnStop() { }
        public override State OnUpdate() {
            if (Value == 0 || Value < 0.001f)
            {
                Value = 0;
                return State.Success;
            }

            Value -= 0.005f;
            return State.Running; 
        }
    }
}
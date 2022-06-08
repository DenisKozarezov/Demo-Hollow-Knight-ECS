using AI.BehaviorTree.Nodes;

namespace Examples.Example_1.FalseKnight.AI.Conditions
{
    public class ConditionLoop : ConditionNode
    {
        private float _currentCount = 0;
        public float Count;

        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override State OnUpdate() 
        {
            return State.Success;
        }

        public override bool Condition()
        {
            if (_currentCount >= Count)
            {
                _currentCount = 0;
                return false;
            }

            _currentCount++;
            return true;
        }
    }
}
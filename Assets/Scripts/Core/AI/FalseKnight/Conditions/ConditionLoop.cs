using AI.BehaviorTree.Nodes;

namespace Core.AI.FalseKnight.Conditions
{
    public class ConditionLoop : ConditionNode
    {
        private float _currentCount = 0f;
        public float Count;

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
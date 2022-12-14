using AI.BehaviourTree.Nodes;
using AI.BehaviourTree.Nodes.Composites;
using Core.AI.FalseKnight.Parameters;

namespace Core.AI.FalseKnight.Sequencers
{
    public class AgressiveAttackSequence : SequencerNode
    {
        public override float Cost(ParameterNode parameter)
        {
            Aggression aggression = parameter as Aggression;

            if (aggression)
            {
                return aggression.Value > 1f ? 5f : 0f;
            }

            return base.Cost();
        }
    }
}
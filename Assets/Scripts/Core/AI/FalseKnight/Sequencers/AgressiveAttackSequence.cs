using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.CompositeNodes;
using Core.AI.FalseKnight.Parameters;

namespace Core.AI.FalseKnight.Sequencers
{
    public class AgressiveAttackSequence : SequencerNode
    {
        public override float Cost(ParameterNode parametr)
        {
            Aggression aggression = parametr as Aggression;

            if (aggression)
            {
                return aggression.Value > 0.5f ? 5f : 0f;
            }

            return 1;
        }
    }
}
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.CompositeNodes;
using Examples.Example_1.FalseKnight.AI.Parameters;

namespace Examples.Example_1.FalseKnight.AI.Sequencers
{
    public class AgressiveAttackSequence : SequencerNode
    {
        public override float Cost(ParameterNode parametr)
        {
            Aggression aggression = parametr as Aggression;

            if (aggression)
            {
                if (aggression.Value > 0.5)
                    return 5;
                return 0;
            }

            return 1;
        }
    }
}
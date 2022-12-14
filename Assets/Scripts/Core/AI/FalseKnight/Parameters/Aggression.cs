using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Core.ECS.Events;
using Leopotam.Ecs;

namespace Core.AI.FalseKnight.Parameters
{
    public class Aggression : FloatNode
    {
        protected override State OnUpdate()
        {
            if (BehaviorTreeRef.Agent.Has<DamageEventComponent>())
            {
                Value += 0.7f;
            }            
            
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
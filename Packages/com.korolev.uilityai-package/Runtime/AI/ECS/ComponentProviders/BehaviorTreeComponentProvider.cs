using AI.ECS.Components;
using Voody.UniLeo;

namespace AI.ECS.ComponentProviders
{
    public sealed class BehaviorTreeComponentProvider : MonoProvider<BehaviorTreeComponent> 
    {
        private void Start()
        {
            //value.BehaviorTree = value.BehaviorTree.Clone();
        }
    }
}
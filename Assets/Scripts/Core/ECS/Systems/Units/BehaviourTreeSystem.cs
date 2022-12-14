using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.ECS.Systems
{
    public sealed class BehaviourTreeSystem : IEcsRunSystem
    {        
        private readonly EcsFilter<BehaviourTreeComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                if (!component.Initialized)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(i);
                    component.BehaviourTree = component.BehaviourTree.Clone();
                    component.BehaviourTree.Init(ref entity);
                    component.Initialized = true;
                }
                component.BehaviourTree.Update();
            }
        }
    }
}
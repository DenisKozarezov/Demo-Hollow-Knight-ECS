/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Components;
using Leopotam.Ecs;

namespace AI.ECS.Systems
{
    public class BehaviorTreeSystem : IEcsRunSystem 
    {        
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<BehaviorTreeComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var component = ref entity.Get<BehaviorTreeComponent>();
                if (!component.Initialized) component.Init(_world);
                component.BehaviorTree.Update();
            }
        }
    }
}
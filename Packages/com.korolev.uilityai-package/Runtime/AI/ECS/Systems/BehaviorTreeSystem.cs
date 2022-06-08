/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Components;
using Leopotam.Ecs;

namespace AI.ECS.Systems
{
    public class BehaviorTreeSystem : IEcsInitSystem, IEcsRunSystem {
        
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<BehaviorTreeComponent> _filter = null;

        public virtual void Init () 
        {
            foreach (var i in _filter) 
            {
                ref var ecsEntity = ref _filter.GetEntity (i);
                ecsEntity.Get<BehaviorTreeComponent>().Init(_world);
            }
        }
        public void Run () 
        {
            foreach (var i in _filter) 
            {
                ref var ecsEntity = ref _filter.GetEntity(i);
                ref var component = ref ecsEntity.Get<BehaviorTreeComponent>();
                component.BehaviorTree.Update();
            }
        }
    }
}
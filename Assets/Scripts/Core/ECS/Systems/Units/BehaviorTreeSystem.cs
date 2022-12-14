/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.ECS.Systems
{
    public class BehaviorTreeSystem : IEcsRunSystem 
    {        
        private readonly EcsFilter<BehaviorTreeComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                if (!component.Initialized)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(i);
                    component.BehaviorTree = component.BehaviorTree.Clone();
                    component.BehaviorTree.Init(ref entity);
                    component.Initialized = true;
                }
                component.BehaviorTree.Update();
            }
        }
    }
}
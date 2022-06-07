/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Components;
using Leopotam.Ecs;

namespace AI.ECS.Systems
{
    public class BehaviorTreeSystem : IEcsInitSystem, IEcsRunSystem {
        
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<BehaviorTreeComponent> _filter = null;

        public virtual void Init () {
            // Сработает на старте
            foreach (var i in _filter) {
                ref var ecsEntity = ref _filter.GetEntity (i);
                ecsEntity.Get<BehaviorTreeComponent>().Init(_world);
                ValidateReferences(ecsEntity);
            }
        }
        private void ValidateReferences(EcsEntity ecsEntity) {
            ecsEntity.Get<BehaviorTreeComponent>().ValidateRefferences();
        }
        public void Run () {
            foreach (var i in _filter) {
                ref var ecsEntity = ref _filter.GetEntity (i);

                if (!ecsEntity.Get<BehaviorTreeComponent>().isInitialised) {
                    ecsEntity.Get<BehaviorTreeComponent>().Init(_world);
                    ValidateReferences(ecsEntity);
                    ecsEntity.Get<BehaviorTreeComponent>().isInitialised = true;
                }
                
                ecsEntity.Get<BehaviorTreeComponent>().BehaviorTree.Update();
            }
        }
    }
}
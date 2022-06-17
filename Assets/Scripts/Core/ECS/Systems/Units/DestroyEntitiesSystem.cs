using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DestroyEntitiesSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                entity.Destroy();
            }
        }
    }
}
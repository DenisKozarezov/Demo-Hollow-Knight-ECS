using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.Player
{
    internal class PlayerHealthModifiedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerHealthModifiedEvent> _filter = null;

        internal PlayerHealthModifiedSystem()
        {

        }
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
            }
        }
    }
}

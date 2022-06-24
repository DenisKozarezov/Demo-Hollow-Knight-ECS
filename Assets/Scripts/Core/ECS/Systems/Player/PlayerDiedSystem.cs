using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;
using Core.ECS.Components;

namespace Core.ECS.Systems.Player
{
    internal class PlayerDiedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerDiedEvent> _filter = null;
        private readonly EcsFilter<RigidbodyComponent, PlayerTagComponent> _player = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                foreach (var pl in _player)
                {
                    // Lock physics
                    Rigidbody2D rigidbody = _player.Get1(pl).Value;
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
    }
}

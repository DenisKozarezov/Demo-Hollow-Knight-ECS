using UnityEngine;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerRespawnSystem/* : IEcsRunSystem*/
    {
        //private readonly EcsWorld _world = null;
        //private readonly EcsFilter<PlayerDiedEvent> _event = null;
        //private readonly EcsFilter<TransformComponent, PlayerTagComponent> _player = null;

        //void IEcsRunSystem.Run()
        //{
        //    foreach (var @event in _event)
        //    {
        //        foreach (var pl in _player)
        //        {
        //            ref TransformComponent player = ref _player.Get1(pl);
        //            player.Value.position = GetNearestBench();

        //            // Camera Fade
        //            _world.NewEntity(new CameraFadeEventComponent { FadeMode = FadeMode.Off, FadeTime = 2f });
        //        }
        //    }
        //}

        //private Vector2 GetNearestBench()
        //{
        //    return Vector2.down;
        //}
    }
}

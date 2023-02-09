using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerDiedSystem/* : IEcsRunSystem*/
    {
        //private readonly EcsWorld _world = null;
        //private readonly EcsFilter<PlayerDiedEvent> _event = null;
        //private readonly EcsFilter<ColliderComponent, PlayerTagComponent> _player = null;

        //private const string DeathBlow = "Prefabs/VFX/Player Death/Low Health Hit";
        //private const string DeathParticle = "Prefabs/VFX/Player Death/Death Effect";

        //private GameObject CreateDeathEffect(Vector2 position)
        //{
        //    var deathBlow = Resources.Load<GameObject>(DeathBlow);
        //    var deathParticle = Resources.Load<GameObject>(DeathParticle);

        //    var effect = GameObject.Instantiate(deathBlow, position, Quaternion.identity);
        //    GameObject.Destroy(effect, 0.7f);
        //    GameObject.Instantiate(deathParticle, position, Quaternion.identity);
        //    return effect;
        //}
        //void IEcsRunSystem.Run()
        //{
        //    foreach (var @event in _event)
        //    {
        //        foreach (var pl in _player)
        //        {
        //            Vector2 position = _player.Get1(pl).Value.bounds.center;

        //            // Death Effect
        //            CreateDeathEffect(position);

        //            // Camera Shake
        //            _world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 5f, ShakeForce = 0.2f });

        //            // Camera Fade
        //            _world.NewEntity(new CameraFadeEventComponent { FadeMode = FadeMode.On, FadeTime = 5f });
        //        }
        //    }
        //}
    }
}

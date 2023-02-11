using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerDiedSystem : ReactiveSystem<GameEntity>
    {
        public PlayerDiedSystem(GameContext game) : base(game) { }

        private readonly string DeathBlow = "Prefabs/VFX/Player Death/Low Health Hit";
        private readonly string DeathParticle = "Prefabs/VFX/Player Death/Death Effect";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var deathBlow = Resources.Load<GameObject>(DeathBlow);
            var deathParticle = Resources.Load<GameObject>(DeathParticle);

            var effect = GameObject.Instantiate(deathBlow, position, Quaternion.identity);
            GameObject.Destroy(effect, 0.7f);
            GameObject.Instantiate(deathParticle, position, Quaternion.identity);
            return effect;
        }
        private void DisableCollision(Collider2D collider)
        {
            collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            collider.enabled = false;
            collider.gameObject.layer = Constants.IgnoreRaycastLayer;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Collider, GameMatcher.Dead));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity player in entities)
            {
                Collider2D collider = player.collider.Value;

                DisableCollision(collider);
                CreateDeathEffect(collider.bounds.center);

                // Camera Shake
                //_world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 5f, ShakeForce = 0.2f });

                // Camera Fade
                //_world.NewEntity(new CameraFadeEventComponent { FadeMode = FadeMode.On, FadeTime = 5f });
            }
        }
    }
}

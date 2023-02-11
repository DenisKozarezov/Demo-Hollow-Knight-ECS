using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerDiedSystem : ReactiveSystem<GameEntity>
    {
        public PlayerDiedSystem(GameContext game) : base(game) { }

        private const string DeathBlow = "Prefabs/VFX/Player Death/Low Health Hit";
        private const string DeathParticle = "Prefabs/VFX/Player Death/Death Effect";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var deathBlow = Resources.Load<GameObject>(DeathBlow);
            var deathParticle = Resources.Load<GameObject>(DeathParticle);

            var effect = GameObject.Instantiate(deathBlow, position, Quaternion.identity);
            GameObject.Destroy(effect, 0.7f);
            GameObject.Instantiate(deathParticle, position, Quaternion.identity);
            return effect;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.SpriteRenderer, GameMatcher.Dead));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity player in entities)
            {
                CreateDeathEffect(player.spriteRenderer.Value.bounds.center);

                // Camera Shake
                //_world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 5f, ShakeForce = 0.2f });

                // Camera Fade
                //_world.NewEntity(new CameraFadeEventComponent { FadeMode = FadeMode.On, FadeTime = 5f });
            }
        }
    }
}

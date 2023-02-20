using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class CreateDustCloudSystem : ReactiveSystem<GameEntity>
    {
        private readonly string PrefabPath = "Prefabs/VFX/Smoke/Dust";
        private readonly GameObject _cloudPrefab;

        public CreateDustCloudSystem(GameContext game) : base(game) 
        {
            _cloudPrefab = Resources.Load<GameObject>(PrefabPath);
        }

        private GameObject InstantiatePrefab(Vector2 point, Vector3 scale) =>
            GameObject
            .Instantiate(_cloudPrefab, point, Quaternion.identity)
            .With(x => x.transform.localScale = scale);

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Grounded.Added());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isGrounded && entity.isUnit && entity.hasSpriteRenderer;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                Bounds bounds = entity.spriteRenderer.Value.bounds;
                Vector3 point = bounds.center + Vector3.down * (bounds.size.y / 2f);
                GameObject.Destroy(InstantiatePrefab(point, Vector2.one), 1f);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class CreateDustCloudSystem : ReactiveSystem<GameEntity>
    {
        private readonly string PrefabPath = "Prefabs/VFX/Smoke/Dust";

        public CreateDustCloudSystem(GameContext game) : base(game) { }

        private GameObject InstantiatePrefab(Vector2 point, Vector3 scale)
        {
            var asset = Resources.Load(PrefabPath) as GameObject;
            GameObject prefab = GameObject.Instantiate(asset, point, Quaternion.identity);
            prefab.transform.localScale = scale;
            return prefab;
        }

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
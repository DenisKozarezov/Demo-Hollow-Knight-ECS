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
            return entity.isGrounded && entity.isUnit;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                Vector3 center = entity.spriteRenderer.Value.bounds.center;
                Collider2D collider = entity.collider.Value;
                Vector3 point = center + Vector3.down * collider.bounds.extents.y;
                GameObject.Destroy(InstantiatePrefab(point, Vector2.one), 1f);
            }
        }
    }
}
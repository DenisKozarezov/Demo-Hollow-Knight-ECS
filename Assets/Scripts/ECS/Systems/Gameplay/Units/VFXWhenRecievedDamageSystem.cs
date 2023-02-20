using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core.ECS.Systems.Player
{
    public sealed class VFXWhenRecievedDamageSystem : ReactiveSystem<GameEntity>
    {
        private readonly string HitEffectPath = "Prefabs/VFX/Impact/Hit Crack Impact";
        private readonly GameObject _smallCrackPrefab;

        public VFXWhenRecievedDamageSystem(GameContext game) : base(game) 
        {
            _smallCrackPrefab = Resources.Load<GameObject>(HitEffectPath);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DamageTaken.Added());
        }
        protected override bool Filter(GameEntity entity)
        {
            return !entity.isInvulnerable && entity.isHittable && entity.hasCollider;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                GameObject.Destroy(CreateEffect(entity.collider.Value.bounds.center), 0.5f);
            }
        }

        private GameObject CreateEffect(Vector2 position) => 
            GameObject.Instantiate(_smallCrackPrefab, position, Quaternion.identity);
    }
}

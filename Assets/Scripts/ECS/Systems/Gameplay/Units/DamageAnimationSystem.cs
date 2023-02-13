using System.Collections.Generic;
using UnityEngine;
using Entitas;
using DG.Tweening;

namespace Core.ECS.Systems
{
    public sealed class DamageAnimationSystem : ReactiveSystem<GameEntity>
    {
        private const float Duration = 0.3f;
        private Color WhiteColor = new Color(1, 1, 1, 0.9f);

        public DamageAnimationSystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.DamageTaken, GameMatcher.CurrentHp, GameMatcher.SpriteRenderer));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCurrentHp;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                SpriteRenderer renderer = entity.spriteRenderer.Value;
                renderer
                    .DOColor(WhiteColor, Duration)
                    .OnComplete(() => renderer.DOColor(Color.black, Duration))
                    .SetEase(Ease.Linear);
            }
        }   
    }
}
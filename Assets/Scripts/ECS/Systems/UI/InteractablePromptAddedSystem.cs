using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Core.ECS.Behaviours;

namespace Core.ECS.Systems.UI
{
    public sealed class InteractablePromptAddedSystem : ReactiveSystem<GameEntity>
    {
        private readonly string PromptPath = "Prefabs/UI/Interactable Prompt";

        public InteractablePromptAddedSystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Collided.Added());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInteractable && entity.hasSpriteRenderer;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                var asset = Resources.Load<InteractablePromptBehaviour>(PromptPath);

                SpriteRenderer renderer = entity.spriteRenderer.Value;
                Vector3 position = renderer.bounds.center + Vector3.up * renderer.size.y * 1.3f;

                var prompt = GameObject.Instantiate(asset, position, Quaternion.identity);
                prompt.SetText(entity.interactable.Label);

                prompt.Fade(FadeMode.On, 0.5f);
            }
        }
    }
}

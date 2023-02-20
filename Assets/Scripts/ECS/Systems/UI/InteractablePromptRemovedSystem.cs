using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems.UI
{
    public sealed class InteractablePromptRemovedSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _prompts;
        public InteractablePromptRemovedSystem(GameContext game) : base(game)
        {
            _prompts = game.GetGroup(GameMatcher.InteractablePrompt);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CanInteract.Removed());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInteractable;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in _prompts)
            {
                entity.interactablePrompt.Value.Fade(FadeMode.Off, 0.5f);
            }
        }
    }
}

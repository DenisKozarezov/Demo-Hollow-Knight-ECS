namespace Core.ECS.Systems.UI
{
    public sealed class UISystems : Feature
    {
        public UISystems(Contexts contexts) : base(nameof(GameplaySystems))
        {
            Add(new HealthViewInitSystem(contexts.game));
            Add(new InteractablePromptAddedSystem(contexts.game));
            Add(new InteractablePromptRemovedSystem(contexts.game));
            //Add(new BossAnnouncementUISystem(gameView));
        }
    }
}

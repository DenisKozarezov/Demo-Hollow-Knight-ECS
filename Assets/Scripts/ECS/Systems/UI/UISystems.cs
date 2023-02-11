namespace Core.ECS.Systems.UI
{
    public sealed class UISystems : Feature
    {
        public UISystems(Contexts contexts) : base(nameof(GameplaySystems))
        {
            Add(new HealthViewInitSystem(contexts.game));
            //Add(new HealthReducedUISystem(healthView));
            //Add(new HealthHealedUISystem(healthView));
            //Add(new InteractablePromptUISystem());
            //Add(new BossAnnouncementUISystem(gameView));
            //Add(new GeoObtainedUISystem(geoView));
        }
    }
}

namespace Core.ECS.Systems.UI
{
    public sealed class UISystems : Feature
    {
        public UISystems(GameContext context) : base(context)
        {
            Add(new HealthViewInitSystem());
            Add(new HealthReducedUISystem());
            Add(new HealthHealedUISystem());
            Add(new InteractablePromptUISystem());
            Add(new GeoObtainedUISystem());
        }
    }
}

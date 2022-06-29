namespace Core.ECS.Systems.UI
{
    internal class UISystems : Feature
    {
        internal UISystems(GameContext context) : base(context)
        {
            Add(new HealthViewInitSystem());
            Add(new HealthReducedUISystem());
            Add(new HealthHealedUISystem());
            Add(new InteractablePromptUISystem());
            Add(new GeoObtainedUISystem());
        }
    }
}

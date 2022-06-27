namespace Core.ECS.Systems.UI
{
    internal class UISystems : Feature
    {
        internal UISystems(GameContext context) : base(context)
        {
            Add(new HealthViewInitSystem());
            Add(new HealthViewReducedSystem());
            Add(new HealthViewHealedSystem());
            Add(new InteractablePromptSystem());
        }
    }
}

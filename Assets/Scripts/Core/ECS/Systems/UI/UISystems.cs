using Core.UI;

namespace Core.ECS.Systems.UI
{
    public sealed class UISystems : Feature
    {
        public UISystems(GameContext context) : base(context)
        {
            HealthUIView healthView = context.DiContainer.Resolve<HealthUIView>();
            GeoUIView geoView = context.DiContainer.Resolve<GeoUIView>();

            Add(new HealthViewInitSystem(healthView));
            Add(new HealthReducedUISystem(healthView));
            Add(new HealthHealedUISystem(healthView));
            Add(new InteractablePromptUISystem());
            Add(new GeoObtainedUISystem(geoView));
        }
    }
}

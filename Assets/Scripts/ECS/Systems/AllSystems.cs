using Core.ECS.Systems.UI;

namespace Core.ECS.Systems
{
    public sealed class AllSystems : Feature
    {
        public AllSystems(Contexts contexts) : base(nameof(GameplaySystems))
        {
            Add(new GameplaySystems(contexts));         
            Add(new UISystems(context));
            Add(new EntitiesCleanupSystem());
        }
    }
}

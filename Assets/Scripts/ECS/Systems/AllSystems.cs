using Core.ECS.Systems.UI;

namespace Core.ECS.Systems
{
    public sealed class AllSystems : Feature
    {
        public AllSystems(Contexts contexts, Services.Services services) : base(nameof(GameplaySystems))
        {
            Add(new GameplaySystems(contexts));         
            Add(new UISystems(contexts));
            //Add(new EntitiesCleanupSystem());
        }
    }
}

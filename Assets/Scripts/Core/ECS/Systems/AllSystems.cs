using Core.ECS.Systems.UI;

namespace Core.ECS.Systems
{
    public sealed class AllSystems : Feature
    {
        public AllSystems(GameContext context) : base(context)
        {
            Add(new GameplaySystems(context));         
            Add(new UISystems(context));
            Add(new EntitiesCleanupSystem());
        }
    }
}

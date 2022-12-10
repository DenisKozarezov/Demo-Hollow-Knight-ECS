using Core.ECS.Systems.UI;

namespace Core.ECS.Systems
{
    internal class AllSystems : Feature
    {
        internal AllSystems(GameContext context) : base(context)
        {
            Add(new GameplaySystems(context));         
            Add(new UISystems(context));
            Add(new EntitiesCleanupSystem());
        }
    }
}

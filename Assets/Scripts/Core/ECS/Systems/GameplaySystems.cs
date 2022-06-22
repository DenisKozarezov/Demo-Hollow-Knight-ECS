using AI.ECS.Systems;
using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Systems.Camera;
using Core.ECS.Systems.Player;
using Core.ECS.Systems.Units;

namespace Core.ECS.Systems
{
    internal class GameplaySystems : Feature
    {
        internal GameplaySystems(GameContext context) : base(context)
        {
            Add(new UnitsSystems(context));
            Add(new HitSystem());
            Add(new DamageSystem());
            Add(new PlayerSystems(context));
            Add(new HealthSystem());
            Add(new GroundSystem());
            Add(new CameraSystems(context));
            Add(new BehaviorTreeSystem());
        }
    }
}

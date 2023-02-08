using Core.ECS.Systems.Camera;
using Core.ECS.Systems.Player;
using Core.ECS.Systems.Units;
using Core.UI;

namespace Core.ECS.Systems
{
    public sealed class GameplaySystems : Feature
    {
        public GameplaySystems(Contexts contexts) : base(nameof(GameplaySystems))
        {
            Add(new UnitsSystems(context));
            Add(new HitSystem());
            Add(new DamageSystem());
            Add(new PlayerSystems(context));
            Add(new HealthSystem());
            Add(new DialogueSystem(context.DiContainer.Resolve<DialogueUIView>()));
            Add(new BehaviourTreeSystem());
            Add(new EnemyDiedSystem());
            Add(new EnemyDroppingGeoSystem(context.DiContainer.Resolve<GeoView.Factory>()));
            Add(new CameraSystems(context, UnityEngine.Camera.main));
        }
    }
}

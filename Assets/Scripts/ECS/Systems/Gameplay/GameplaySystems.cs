using Core.ECS.Systems.Player;
using Core.ECS.Systems.Units;

namespace Core.ECS.Systems
{
    public sealed class GameplaySystems : Feature
    {
        public GameplaySystems(Contexts contexts) : base(nameof(GameplaySystems))
        {
            Add(new EmitInputSystem(contexts.input));

            //Add(new HitSystem());
            //Add(new DamageSystem());
            Add(new HealthSystem(contexts.game));
            Add(new UnitsSystems(contexts));
            Add(new PlayerSystems(contexts));
            //Add(new DialogueSystem(context.DiContainer.Resolve<DialogueUIView>()));
            //Add(new BehaviourTreeSystem());
            //Add(new EnemyDiedSystem());
            //Add(new EnemyDroppingGeoSystem(context.DiContainer.Resolve<GeoView.Factory>()));
            //Add(new CameraSystems(contexts, UnityEngine.Camera.main));
        }
    }
}

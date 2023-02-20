using Core.ECS.Systems.Camera;
using Core.ECS.Systems.Player;
using Core.ECS.Systems.Units;

namespace Core.ECS.Systems
{
    public sealed class GameplaySystems : Feature
    {
        public GameplaySystems(Contexts contexts, Services.Services services) : base(nameof(GameplaySystems))
        {
            Add(new EmitInputSystem(contexts.input));

            Add(new HealthSystem(contexts.game));
            Add(new UnitsSystems(contexts));
            Add(new PlayerSystems(contexts));
            Add(new DamageSystem(contexts.game));
            Add(new VFXWhenRecievedDamageSystem(contexts.game));
            //Add(new DialogueSystem(context.DiContainer.Resolve<DialogueUIView>()));
            Add(new BehaviourTreeSystem(contexts.game));
            //Add(new EnemyDiedSystem());
            Add(new EnemyDroppingGeoSystem(contexts.game, services.DiContainer.Resolve<Behaviours.GeoView.Factory>()));
            Add(new CameraSystems(contexts));
        }
    }
}

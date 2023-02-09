//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class GameEventSystems : Feature {

    public GameEventSystems(Contexts contexts) {
        Add(new AnimateDamageEventEventSystem(contexts)); // priority: 0
        Add(new AttackingEventSystem(contexts)); // priority: 0
        Add(new CameraFadeEventEventSystem(contexts)); // priority: 0
        Add(new CameraShakeEventEventSystem(contexts)); // priority: 0
        Add(new CreateDustEventEventSystem(contexts)); // priority: 0
        Add(new CreateUnitEventEventSystem(contexts)); // priority: 0
        Add(new DamageEventEventSystem(contexts)); // priority: 0
        Add(new DamageTakenEventSystem(contexts)); // priority: 0
        Add(new DestroyedEventSystem(contexts)); // priority: 0
        Add(new DiedEventSystem(contexts)); // priority: 0
        Add(new DirectionEventSystem(contexts)); // priority: 0
        Add(new EnergyReducedEventSystem(contexts)); // priority: 0
        Add(new EnteredBossZoneEventSystem(contexts)); // priority: 0
        Add(new FalseKnightAttackEventEventSystem(contexts)); // priority: 0
        Add(new FalseKnightJumpEventEventSystem(contexts)); // priority: 0
        Add(new FalseKnightRollEventEventSystem(contexts)); // priority: 0
        Add(new FalseKnightStrongAttackEventEventSystem(contexts)); // priority: 0
        Add(new GroundedEventSystem(contexts)); // priority: 0
        Add(new HitEventEventSystem(contexts)); // priority: 0
        Add(new InteractableTriggerEnterEventEventSystem(contexts)); // priority: 0
        Add(new InteractableTriggerExitEventEventSystem(contexts)); // priority: 0
        Add(new JumpingEventSystem(contexts)); // priority: 0
        Add(new MovingEventSystem(contexts)); // priority: 0
        Add(new ObtainedGeoEventSystem(contexts)); // priority: 0
        Add(new PlayerTalkingWithNPCEventEventSystem(contexts)); // priority: 0
        Add(new RecievedDamageEventSystem(contexts)); // priority: 0
        Add(new RestoredHealthEventSystem(contexts)); // priority: 0
        Add(new StoppedMovingEventSystem(contexts)); // priority: 0
    }
}

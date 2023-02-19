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
        Add(new AttackingEventSystem(contexts)); // priority: 0
        Add(new AnyCameraFadeEventSystem(contexts)); // priority: 0
        Add(new AnyCameraShakeEventSystem(contexts)); // priority: 0
        Add(new DamageTakenEventSystem(contexts)); // priority: 0
        Add(new DestroyedEventSystem(contexts)); // priority: 0
        Add(new DiedEventSystem(contexts)); // priority: 0
        Add(new DirectionEventSystem(contexts)); // priority: 0
        Add(new AnyEnergyReducedEventSystem(contexts)); // priority: 0
        Add(new AnyEnteredBossZoneEventSystem(contexts)); // priority: 0
        Add(new GroundedEventSystem(contexts)); // priority: 0
        Add(new HitEventEventSystem(contexts)); // priority: 0
        Add(new JumpingEventSystem(contexts)); // priority: 0
        Add(new MovingEventSystem(contexts)); // priority: 0
        Add(new AnyPlayerTalkingWithNPCEventEventSystem(contexts)); // priority: 0
        Add(new PositionEventSystem(contexts)); // priority: 0
        Add(new RestoredHealthEventSystem(contexts)); // priority: 0
        Add(new StoppedMovingEventSystem(contexts)); // priority: 0
    }
}

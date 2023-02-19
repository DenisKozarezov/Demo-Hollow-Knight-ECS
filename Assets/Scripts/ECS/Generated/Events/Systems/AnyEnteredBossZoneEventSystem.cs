//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AnyEnteredBossZoneEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly Entitas.IGroup<GameEntity> _listeners;
    readonly System.Collections.Generic.List<GameEntity> _entityBuffer;
    readonly System.Collections.Generic.List<IAnyEnteredBossZoneListener> _listenerBuffer;

    public AnyEnteredBossZoneEventSystem(Contexts contexts) : base(contexts.game) {
        _listeners = contexts.game.GetGroup(GameMatcher.AnyEnteredBossZoneListener);
        _entityBuffer = new System.Collections.Generic.List<GameEntity>();
        _listenerBuffer = new System.Collections.Generic.List<IAnyEnteredBossZoneListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.EnteredBossZone)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasEnteredBossZone;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.enteredBossZone;
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.anyEnteredBossZoneListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAnyEnteredBossZone(e, component.BossModel);
                }
            }
        }
    }
}
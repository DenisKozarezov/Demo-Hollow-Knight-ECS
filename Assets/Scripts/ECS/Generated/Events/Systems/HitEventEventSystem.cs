//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class HitEventEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IHitEventListener> _listenerBuffer;

    public HitEventEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IHitEventListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.HitEvent)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasHitEvent && entity.hasHitEventListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.hitEvent;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.hitEventListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnHitEvent(e, component.Damage, component.HitPosition, component.HitRadius, component.TargetLayer, component.Source);
            }
        }
    }
}
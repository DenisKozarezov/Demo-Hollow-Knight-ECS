//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Components.Units.HitEventComponent hitEvent { get { return (Core.ECS.Components.Units.HitEventComponent)GetComponent(GameComponentsLookup.HitEvent); } }
    public bool hasHitEvent { get { return HasComponent(GameComponentsLookup.HitEvent); } }

    public void AddHitEvent(int newDamage, UnityEngine.Vector2 newHitPosition, float newHitRadius, int newTargetLayer, UnityEngine.GameObject newSource) {
        var index = GameComponentsLookup.HitEvent;
        var component = (Core.ECS.Components.Units.HitEventComponent)CreateComponent(index, typeof(Core.ECS.Components.Units.HitEventComponent));
        component.Damage = newDamage;
        component.HitPosition = newHitPosition;
        component.HitRadius = newHitRadius;
        component.TargetLayer = newTargetLayer;
        component.Source = newSource;
        AddComponent(index, component);
    }

    public void ReplaceHitEvent(int newDamage, UnityEngine.Vector2 newHitPosition, float newHitRadius, int newTargetLayer, UnityEngine.GameObject newSource) {
        var index = GameComponentsLookup.HitEvent;
        var component = (Core.ECS.Components.Units.HitEventComponent)CreateComponent(index, typeof(Core.ECS.Components.Units.HitEventComponent));
        component.Damage = newDamage;
        component.HitPosition = newHitPosition;
        component.HitRadius = newHitRadius;
        component.TargetLayer = newTargetLayer;
        component.Source = newSource;
        ReplaceComponent(index, component);
    }

    public void RemoveHitEvent() {
        RemoveComponent(GameComponentsLookup.HitEvent);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherHitEvent;

    public static Entitas.IMatcher<GameEntity> HitEvent {
        get {
            if (_matcherHitEvent == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HitEvent);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHitEvent = matcher;
            }

            return _matcherHitEvent;
        }
    }
}

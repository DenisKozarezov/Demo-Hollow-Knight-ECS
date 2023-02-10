//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Events.CreateUnitEventComponent createUnitEvent { get { return (Core.ECS.Events.CreateUnitEventComponent)GetComponent(GameComponentsLookup.CreateUnitEvent); } }
    public bool hasCreateUnitEvent { get { return HasComponent(GameComponentsLookup.CreateUnitEvent); } }

    public void AddCreateUnitEvent(uint newID, UnityEngine.Vector2 newPoint) {
        var index = GameComponentsLookup.CreateUnitEvent;
        var component = (Core.ECS.Events.CreateUnitEventComponent)CreateComponent(index, typeof(Core.ECS.Events.CreateUnitEventComponent));
        component.ID = newID;
        component.Point = newPoint;
        AddComponent(index, component);
    }

    public void ReplaceCreateUnitEvent(uint newID, UnityEngine.Vector2 newPoint) {
        var index = GameComponentsLookup.CreateUnitEvent;
        var component = (Core.ECS.Events.CreateUnitEventComponent)CreateComponent(index, typeof(Core.ECS.Events.CreateUnitEventComponent));
        component.ID = newID;
        component.Point = newPoint;
        ReplaceComponent(index, component);
    }

    public void RemoveCreateUnitEvent() {
        RemoveComponent(GameComponentsLookup.CreateUnitEvent);
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

    static Entitas.IMatcher<GameEntity> _matcherCreateUnitEvent;

    public static Entitas.IMatcher<GameEntity> CreateUnitEvent {
        get {
            if (_matcherCreateUnitEvent == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CreateUnitEvent);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCreateUnitEvent = matcher;
            }

            return _matcherCreateUnitEvent;
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Components.UI.GeoUI geoUI { get { return (Core.ECS.Components.UI.GeoUI)GetComponent(GameComponentsLookup.GeoUI); } }
    public bool hasGeoUI { get { return HasComponent(GameComponentsLookup.GeoUI); } }

    public void AddGeoUI(Core.ECS.Behaviours.GeoUIView newValue) {
        var index = GameComponentsLookup.GeoUI;
        var component = (Core.ECS.Components.UI.GeoUI)CreateComponent(index, typeof(Core.ECS.Components.UI.GeoUI));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGeoUI(Core.ECS.Behaviours.GeoUIView newValue) {
        var index = GameComponentsLookup.GeoUI;
        var component = (Core.ECS.Components.UI.GeoUI)CreateComponent(index, typeof(Core.ECS.Components.UI.GeoUI));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGeoUI() {
        RemoveComponent(GameComponentsLookup.GeoUI);
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

    static Entitas.IMatcher<GameEntity> _matcherGeoUI;

    public static Entitas.IMatcher<GameEntity> GeoUI {
        get {
            if (_matcherGeoUI == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GeoUI);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGeoUI = matcher;
            }

            return _matcherGeoUI;
        }
    }
}

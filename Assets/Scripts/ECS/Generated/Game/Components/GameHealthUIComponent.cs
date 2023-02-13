//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Components.UI.HealthUI healthUI { get { return (Core.ECS.Components.UI.HealthUI)GetComponent(GameComponentsLookup.HealthUI); } }
    public bool hasHealthUI { get { return HasComponent(GameComponentsLookup.HealthUI); } }

    public void AddHealthUI(Core.ECS.Behaviours.HealthUIView newValue) {
        var index = GameComponentsLookup.HealthUI;
        var component = (Core.ECS.Components.UI.HealthUI)CreateComponent(index, typeof(Core.ECS.Components.UI.HealthUI));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceHealthUI(Core.ECS.Behaviours.HealthUIView newValue) {
        var index = GameComponentsLookup.HealthUI;
        var component = (Core.ECS.Components.UI.HealthUI)CreateComponent(index, typeof(Core.ECS.Components.UI.HealthUI));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveHealthUI() {
        RemoveComponent(GameComponentsLookup.HealthUI);
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

    static Entitas.IMatcher<GameEntity> _matcherHealthUI;

    public static Entitas.IMatcher<GameEntity> HealthUI {
        get {
            if (_matcherHealthUI == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HealthUI);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHealthUI = matcher;
            }

            return _matcherHealthUI;
        }
    }
}
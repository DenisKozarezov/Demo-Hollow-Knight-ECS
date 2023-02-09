//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Components.Player.RecievedDamage recievedDamage { get { return (Core.ECS.Components.Player.RecievedDamage)GetComponent(GameComponentsLookup.RecievedDamage); } }
    public bool hasRecievedDamage { get { return HasComponent(GameComponentsLookup.RecievedDamage); } }

    public void AddRecievedDamage(int newValue) {
        var index = GameComponentsLookup.RecievedDamage;
        var component = (Core.ECS.Components.Player.RecievedDamage)CreateComponent(index, typeof(Core.ECS.Components.Player.RecievedDamage));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRecievedDamage(int newValue) {
        var index = GameComponentsLookup.RecievedDamage;
        var component = (Core.ECS.Components.Player.RecievedDamage)CreateComponent(index, typeof(Core.ECS.Components.Player.RecievedDamage));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRecievedDamage() {
        RemoveComponent(GameComponentsLookup.RecievedDamage);
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

    static Entitas.IMatcher<GameEntity> _matcherRecievedDamage;

    public static Entitas.IMatcher<GameEntity> RecievedDamage {
        get {
            if (_matcherRecievedDamage == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RecievedDamage);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRecievedDamage = matcher;
            }

            return _matcherRecievedDamage;
        }
    }
}

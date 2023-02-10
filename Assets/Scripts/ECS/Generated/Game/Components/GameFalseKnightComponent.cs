//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Core.ECS.Components.Units.FalseKnight falseKnightComponent = new Core.ECS.Components.Units.FalseKnight();

    public bool isFalseKnight {
        get { return HasComponent(GameComponentsLookup.FalseKnight); }
        set {
            if (value != isFalseKnight) {
                var index = GameComponentsLookup.FalseKnight;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : falseKnightComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherFalseKnight;

    public static Entitas.IMatcher<GameEntity> FalseKnight {
        get {
            if (_matcherFalseKnight == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FalseKnight);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFalseKnight = matcher;
            }

            return _matcherFalseKnight;
        }
    }
}
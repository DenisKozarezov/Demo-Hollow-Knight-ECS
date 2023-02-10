//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Core.ECS.Events.FalseKnight.FalseKnightRollEvent falseKnightRollEventComponent = new Core.ECS.Events.FalseKnight.FalseKnightRollEvent();

    public bool isFalseKnightRollEvent {
        get { return HasComponent(GameComponentsLookup.FalseKnightRollEvent); }
        set {
            if (value != isFalseKnightRollEvent) {
                var index = GameComponentsLookup.FalseKnightRollEvent;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : falseKnightRollEventComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherFalseKnightRollEvent;

    public static Entitas.IMatcher<GameEntity> FalseKnightRollEvent {
        get {
            if (_matcherFalseKnightRollEvent == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FalseKnightRollEvent);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFalseKnightRollEvent = matcher;
            }

            return _matcherFalseKnightRollEvent;
        }
    }
}
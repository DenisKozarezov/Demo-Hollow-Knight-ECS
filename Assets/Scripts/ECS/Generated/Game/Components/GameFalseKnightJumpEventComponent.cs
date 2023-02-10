//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Core.ECS.Events.FalseKnight.FalseKnightJumpEvent falseKnightJumpEventComponent = new Core.ECS.Events.FalseKnight.FalseKnightJumpEvent();

    public bool isFalseKnightJumpEvent {
        get { return HasComponent(GameComponentsLookup.FalseKnightJumpEvent); }
        set {
            if (value != isFalseKnightJumpEvent) {
                var index = GameComponentsLookup.FalseKnightJumpEvent;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : falseKnightJumpEventComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherFalseKnightJumpEvent;

    public static Entitas.IMatcher<GameEntity> FalseKnightJumpEvent {
        get {
            if (_matcherFalseKnightJumpEvent == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FalseKnightJumpEvent);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFalseKnightJumpEvent = matcher;
            }

            return _matcherFalseKnightJumpEvent;
        }
    }
}
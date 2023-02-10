//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Core.ECS.Components.Units.UnitComponent unitComponent = new Core.ECS.Components.Units.UnitComponent();

    public bool isUnit {
        get { return HasComponent(GameComponentsLookup.Unit); }
        set {
            if (value != isUnit) {
                var index = GameComponentsLookup.Unit;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : unitComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherUnit;

    public static Entitas.IMatcher<GameEntity> Unit {
        get {
            if (_matcherUnit == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Unit);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUnit = matcher;
            }

            return _matcherUnit;
        }
    }
}
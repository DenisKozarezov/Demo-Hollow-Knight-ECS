//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly Core.ECS.Components.Player.Mouse mouseComponent = new Core.ECS.Components.Player.Mouse();

    public bool isMouse {
        get { return HasComponent(InputComponentsLookup.Mouse); }
        set {
            if (value != isMouse) {
                var index = InputComponentsLookup.Mouse;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : mouseComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherMouse;

    public static Entitas.IMatcher<InputEntity> Mouse {
        get {
            if (_matcherMouse == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Mouse);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherMouse = matcher;
            }

            return _matcherMouse;
        }
    }
}
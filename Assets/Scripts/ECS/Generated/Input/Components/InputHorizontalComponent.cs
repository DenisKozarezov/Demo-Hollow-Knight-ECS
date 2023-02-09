//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Core.ECS.Components.Player.Horizontal horizontal { get { return (Core.ECS.Components.Player.Horizontal)GetComponent(InputComponentsLookup.Horizontal); } }
    public bool hasHorizontal { get { return HasComponent(InputComponentsLookup.Horizontal); } }

    public void AddHorizontal(float newValue) {
        var index = InputComponentsLookup.Horizontal;
        var component = (Core.ECS.Components.Player.Horizontal)CreateComponent(index, typeof(Core.ECS.Components.Player.Horizontal));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceHorizontal(float newValue) {
        var index = InputComponentsLookup.Horizontal;
        var component = (Core.ECS.Components.Player.Horizontal)CreateComponent(index, typeof(Core.ECS.Components.Player.Horizontal));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveHorizontal() {
        RemoveComponent(InputComponentsLookup.Horizontal);
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

    static Entitas.IMatcher<InputEntity> _matcherHorizontal;

    public static Entitas.IMatcher<InputEntity> Horizontal {
        get {
            if (_matcherHorizontal == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Horizontal);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherHorizontal = matcher;
            }

            return _matcherHorizontal;
        }
    }
}

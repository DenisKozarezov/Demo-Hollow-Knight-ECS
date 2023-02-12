//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnyObtainedGeoListenerComponent anyObtainedGeoListener { get { return (AnyObtainedGeoListenerComponent)GetComponent(GameComponentsLookup.AnyObtainedGeoListener); } }
    public bool hasAnyObtainedGeoListener { get { return HasComponent(GameComponentsLookup.AnyObtainedGeoListener); } }

    public void AddAnyObtainedGeoListener(System.Collections.Generic.List<IAnyObtainedGeoListener> newValue) {
        var index = GameComponentsLookup.AnyObtainedGeoListener;
        var component = (AnyObtainedGeoListenerComponent)CreateComponent(index, typeof(AnyObtainedGeoListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyObtainedGeoListener(System.Collections.Generic.List<IAnyObtainedGeoListener> newValue) {
        var index = GameComponentsLookup.AnyObtainedGeoListener;
        var component = (AnyObtainedGeoListenerComponent)CreateComponent(index, typeof(AnyObtainedGeoListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyObtainedGeoListener() {
        RemoveComponent(GameComponentsLookup.AnyObtainedGeoListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnyObtainedGeoListener;

    public static Entitas.IMatcher<GameEntity> AnyObtainedGeoListener {
        get {
            if (_matcherAnyObtainedGeoListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyObtainedGeoListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyObtainedGeoListener = matcher;
            }

            return _matcherAnyObtainedGeoListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddAnyObtainedGeoListener(IAnyObtainedGeoListener value) {
        var listeners = hasAnyObtainedGeoListener
            ? anyObtainedGeoListener.value
            : new System.Collections.Generic.List<IAnyObtainedGeoListener>();
        listeners.Add(value);
        ReplaceAnyObtainedGeoListener(listeners);
    }

    public void RemoveAnyObtainedGeoListener(IAnyObtainedGeoListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyObtainedGeoListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyObtainedGeoListener();
        } else {
            ReplaceAnyObtainedGeoListener(listeners);
        }
    }
}

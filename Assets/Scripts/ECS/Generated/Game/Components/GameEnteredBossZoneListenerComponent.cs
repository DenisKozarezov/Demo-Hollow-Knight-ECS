//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public EnteredBossZoneListenerComponent enteredBossZoneListener { get { return (EnteredBossZoneListenerComponent)GetComponent(GameComponentsLookup.EnteredBossZoneListener); } }
    public bool hasEnteredBossZoneListener { get { return HasComponent(GameComponentsLookup.EnteredBossZoneListener); } }

    public void AddEnteredBossZoneListener(System.Collections.Generic.List<IEnteredBossZoneListener> newValue) {
        var index = GameComponentsLookup.EnteredBossZoneListener;
        var component = (EnteredBossZoneListenerComponent)CreateComponent(index, typeof(EnteredBossZoneListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceEnteredBossZoneListener(System.Collections.Generic.List<IEnteredBossZoneListener> newValue) {
        var index = GameComponentsLookup.EnteredBossZoneListener;
        var component = (EnteredBossZoneListenerComponent)CreateComponent(index, typeof(EnteredBossZoneListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveEnteredBossZoneListener() {
        RemoveComponent(GameComponentsLookup.EnteredBossZoneListener);
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

    static Entitas.IMatcher<GameEntity> _matcherEnteredBossZoneListener;

    public static Entitas.IMatcher<GameEntity> EnteredBossZoneListener {
        get {
            if (_matcherEnteredBossZoneListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EnteredBossZoneListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEnteredBossZoneListener = matcher;
            }

            return _matcherEnteredBossZoneListener;
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

    public void AddEnteredBossZoneListener(IEnteredBossZoneListener value) {
        var listeners = hasEnteredBossZoneListener
            ? enteredBossZoneListener.value
            : new System.Collections.Generic.List<IEnteredBossZoneListener>();
        listeners.Add(value);
        ReplaceEnteredBossZoneListener(listeners);
    }

    public void RemoveEnteredBossZoneListener(IEnteredBossZoneListener value, bool removeComponentWhenEmpty = true) {
        var listeners = enteredBossZoneListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveEnteredBossZoneListener();
        } else {
            ReplaceEnteredBossZoneListener(listeners);
        }
    }
}
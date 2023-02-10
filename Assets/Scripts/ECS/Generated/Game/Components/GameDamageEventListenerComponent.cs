//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DamageEventListenerComponent damageEventListener { get { return (DamageEventListenerComponent)GetComponent(GameComponentsLookup.DamageEventListener); } }
    public bool hasDamageEventListener { get { return HasComponent(GameComponentsLookup.DamageEventListener); } }

    public void AddDamageEventListener(System.Collections.Generic.List<IDamageEventListener> newValue) {
        var index = GameComponentsLookup.DamageEventListener;
        var component = (DamageEventListenerComponent)CreateComponent(index, typeof(DamageEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDamageEventListener(System.Collections.Generic.List<IDamageEventListener> newValue) {
        var index = GameComponentsLookup.DamageEventListener;
        var component = (DamageEventListenerComponent)CreateComponent(index, typeof(DamageEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDamageEventListener() {
        RemoveComponent(GameComponentsLookup.DamageEventListener);
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

    static Entitas.IMatcher<GameEntity> _matcherDamageEventListener;

    public static Entitas.IMatcher<GameEntity> DamageEventListener {
        get {
            if (_matcherDamageEventListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DamageEventListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDamageEventListener = matcher;
            }

            return _matcherDamageEventListener;
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

    public void AddDamageEventListener(IDamageEventListener value) {
        var listeners = hasDamageEventListener
            ? damageEventListener.value
            : new System.Collections.Generic.List<IDamageEventListener>();
        listeners.Add(value);
        ReplaceDamageEventListener(listeners);
    }

    public void RemoveDamageEventListener(IDamageEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = damageEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveDamageEventListener();
        } else {
            ReplaceDamageEventListener(listeners);
        }
    }
}
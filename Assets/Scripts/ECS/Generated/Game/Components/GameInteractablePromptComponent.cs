//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Core.ECS.Components.InteractablePrompt interactablePrompt { get { return (Core.ECS.Components.InteractablePrompt)GetComponent(GameComponentsLookup.InteractablePrompt); } }
    public bool hasInteractablePrompt { get { return HasComponent(GameComponentsLookup.InteractablePrompt); } }

    public void AddInteractablePrompt(Core.ECS.Behaviours.InteractablePromptBehaviour newValue) {
        var index = GameComponentsLookup.InteractablePrompt;
        var component = (Core.ECS.Components.InteractablePrompt)CreateComponent(index, typeof(Core.ECS.Components.InteractablePrompt));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceInteractablePrompt(Core.ECS.Behaviours.InteractablePromptBehaviour newValue) {
        var index = GameComponentsLookup.InteractablePrompt;
        var component = (Core.ECS.Components.InteractablePrompt)CreateComponent(index, typeof(Core.ECS.Components.InteractablePrompt));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInteractablePrompt() {
        RemoveComponent(GameComponentsLookup.InteractablePrompt);
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

    static Entitas.IMatcher<GameEntity> _matcherInteractablePrompt;

    public static Entitas.IMatcher<GameEntity> InteractablePrompt {
        get {
            if (_matcherInteractablePrompt == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InteractablePrompt);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInteractablePrompt = matcher;
            }

            return _matcherInteractablePrompt;
        }
    }
}
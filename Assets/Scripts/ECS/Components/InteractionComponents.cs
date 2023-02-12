using Core.ECS.Behaviours;
using Entitas;

namespace Core.ECS.Components
{
    public sealed class InteractablePrompt : IComponent { public InteractablePromptBehaviour Value; }
    public sealed class Interactable : IComponent { public string Label; public InteractType InteractType; }
    
    // Items
    public sealed class Geo : IComponent { public ushort Value; }
}

using Core.UI;
using Entitas;

namespace Core.ECS.Components
{
    public sealed class InteractablePrompt : IComponent { public InteractablePromptBehaviour Value; }
    public sealed class Interactable : IComponent { public string Label; public InteractType InteractType; }
}

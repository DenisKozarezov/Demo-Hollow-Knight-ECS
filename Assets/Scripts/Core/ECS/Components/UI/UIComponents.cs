using System;
using Core.UI;
using TMPro;

namespace Core.ECS.Components.UI
{
    [Serializable] public struct GameViewComponent { public GameUIView View; }
    [Serializable] public struct DialogueViewComponent { public DialogueUIView View; }
    [Serializable] public struct InteractablePromptComponent { public TextMeshPro Label; }
}

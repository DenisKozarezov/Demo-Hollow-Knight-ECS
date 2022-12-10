using System;
using Core.UI;
using TMPro;

namespace Core.ECS.Components.UI
{
    [Serializable] public struct HealthViewComponent { public HealthView View; }
    [Serializable] public struct GameViewComponent { public GameView View; }
    [Serializable] public struct GeoViewComponent { public GeoUIView View; }
    [Serializable] public struct DialogueViewComponent { public DialogueView View; }
    [Serializable] public struct InteractablePromptComponent { public TextMeshPro Label; }
}

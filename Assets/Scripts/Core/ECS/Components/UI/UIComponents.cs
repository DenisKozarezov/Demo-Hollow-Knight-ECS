using System;
using Core.UI;

namespace Core.ECS.Components.UI
{
    [Serializable] internal struct HealthViewComponent { public HealthView View; }
    [Serializable] internal struct GameViewComponent { public GameView View; }
    [Serializable] internal struct GeoViewComponent { public GeoView View; }
    [Serializable] internal struct DialogueViewComponent { public DialogueView View; }
}

using Core.UI;

namespace Core.ECS.Events
{
    internal struct InteractableTriggerEnterEvent { public InteractableView View; public float OffsetY; }
    internal struct InteractableTriggerExitEvent { public InteractableView View; }
}

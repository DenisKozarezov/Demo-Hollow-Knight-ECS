using Leopotam.Ecs;
using Core.UI;
using UnityEngine;

namespace Core.ECS.Events
{
    internal struct InteractableTriggerEnterEvent { public InteractableView View; public Vector2 LocalPosition; }
    internal struct InteractableTriggerExitEvent { public InteractableView View; }
}

using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components;

namespace Core.ECS.Events
{
    public struct InteractableTriggerEnterEvent 
    {
        public Vector2 Position;
        public EcsEntity InteractableEntity;
        public InteractableComponent InteractableComponent;
    }
    public struct InteractableTriggerExitEvent : IEcsIgnoreInFilter { }
}

using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events
{
    [Event(Self)] public sealed class InteractableTriggerEnterEvent : IComponent
    {
        public Vector2 Position;
        public EcsEntity InteractableEntity;
        public InteractableComponent InteractableComponent;
    }
    [Event(Self)] public sealed class InteractableTriggerExitEvent : IComponent { }
}

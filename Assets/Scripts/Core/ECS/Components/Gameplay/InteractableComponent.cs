using System;

namespace Core.ECS.Components
{
    [Serializable] public struct InteractableComponent 
    {
        public bool IsInteractable;
        public string Label;
        public float OffsetY;
        public InteractType InteractType;
    }
}

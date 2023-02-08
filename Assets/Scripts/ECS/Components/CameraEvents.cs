using Entitas;
using Entitas.CodeGeneration.Attributes;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events
{
    [Event(Self)] public sealed class CameraShakeEventComponent : IComponent
    {
        public float ShakeDuration;
        public float ShakeForce;
    }
    [Event(Self)] public sealed class CameraFadeEventComponent : IComponent
    { 
        public float FadeTime; 
        public FadeMode FadeMode;
    }
}
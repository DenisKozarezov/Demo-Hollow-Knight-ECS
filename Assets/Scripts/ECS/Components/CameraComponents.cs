using Entitas;
using Entitas.CodeGeneration.Attributes;
using static Entitas.CodeGeneration.Attributes.EventTarget;
using static Entitas.CodeGeneration.Attributes.CleanupMode;

namespace Core.ECS.Events
{
    [Event(Any), Cleanup(DestroyEntity)] public sealed class CameraShake : IComponent
    {
        public float ShakeDuration;
        public float ShakeForce;
    }
    [Event(Any), Cleanup(DestroyEntity)] public sealed class CameraFade : IComponent
    { 
        public float FadeTime; 
        public FadeMode FadeMode;
    }
}
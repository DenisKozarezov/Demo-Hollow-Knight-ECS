using Entitas;
using Core.ECS.Behaviours;

namespace Core.ECS.Components.UI
{
    public sealed class HealthUI : IComponent { public HealthUIView Value; }
    public sealed class Vignette : IComponent { public UnityEngine.Rendering.Universal.Vignette Value; }
}

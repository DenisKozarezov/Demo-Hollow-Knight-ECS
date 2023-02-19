using Entitas;
using Core.ECS.Behaviours;

namespace Core.ECS.Components.UI
{
    public sealed class HealthUI : IComponent { public HealthUIView Value; }
    public sealed class GameUI : IComponent { public GameUIView Value; }
    public sealed class GeoUI : IComponent { public GeoUIView Value; }
    public sealed class AddingGeo : IComponent { public int Value; }
    public sealed class Vignette : IComponent { public UnityEngine.Rendering.Universal.Vignette Value; }
}

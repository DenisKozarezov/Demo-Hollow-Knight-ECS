using UnityEngine;
using UnityEngine.Rendering.Universal;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Camera
{
    public class CameraLowHealthVignetteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, PlayerTagComponent> _filter = null;
        private readonly Vignette _vignette;
        private float _currentVelocity;

        private const float IntensityMax = 0.55f;

        public CameraLowHealthVignetteSystem(UnityEngine.Camera camera)
        {
            _vignette = camera.GetPostProcessSetting<Vignette>();
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);

                if (health.Health == 0 || health.Health == health.MaxHealth) continue;

                float factor = Mathf.Clamp(1f - (float)health.Health / health.MaxHealth, 0f, IntensityMax);
                _vignette.intensity.value = Mathf.SmoothDamp(_vignette.intensity.value, factor, ref _currentVelocity, 0.3f);
            }
        }
    }
}

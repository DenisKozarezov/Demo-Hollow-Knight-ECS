using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Leopotam.Ecs;
using Core.ECS.Components;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Camera
{
    internal class CameraLowHealthVignetteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, PlayerTagComponent> _filter = null;
        private readonly Vignette _vignette;
        private float _currentVelocity;

        private const float IntensityMax = 0.55f;

        internal CameraLowHealthVignetteSystem(UnityEngine.Camera camera)
        {
            _vignette = camera.GetPostProcessSetting<Vignette>();
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);

                if (health.Health == 0 || health.Health == health.MaxHealth) continue;

                float factor = Mathf.Clamp(1 - (float)health.Health / (float)health.MaxHealth, 0f, IntensityMax);
                _vignette.intensity.value = Mathf.SmoothDamp(_vignette.intensity.value, factor, ref _currentVelocity, 0.3f);
            }
        }
    }
}

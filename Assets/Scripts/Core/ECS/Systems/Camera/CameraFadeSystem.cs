using Leopotam.Ecs;
using Core.UI;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraFadeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CameraFadeEventComponent> _event = null;
        private readonly Fader _fader;

        public CameraFadeSystem(Fader fader)
        {
            _fader = fader;
        }

        void IEcsInitSystem.Init()
        {
            _fader.Fade(FadeMode.On, 0f);
            _fader.Fade(FadeMode.Off, 3f);
        }
        void IEcsRunSystem.Run()
        {
            foreach (var i in _event)
            {
                ref FadeMode mode = ref _event.Get1(i).FadeMode;
                ref float fadeTime = ref _event.Get1(i).FadeTime;
                _fader.Fade(mode, fadeTime);
            }
        }
    }
}

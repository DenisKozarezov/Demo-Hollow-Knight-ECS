using Core.UI;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraFadeSystem
    {
        //private readonly EcsFilter<CameraFadeEventComponent> _event = null;
        //private readonly Fader _fade;

        //public CameraFadeSystem(Fader fade)
        //{
        //    _fade = fade;
        //}

        //void IEcsInitSystem.Init()
        //{
        //    _fade.Fade(FadeMode.On, 0f);
        //    _fade.Fade(FadeMode.Off, 3f);
        //}
        //void IEcsRunSystem.Run()
        //{
        //    foreach (var i in _event)
        //    {
        //        ref FadeMode mode = ref _event.Get1(i).FadeMode;
        //        ref float fadeTime = ref _event.Get1(i).FadeTime;
        //        _fade.Fade(mode, fadeTime);
        //    }
        //}
    }
}

using Core.ECS.Events;
using Leopotam.Ecs;
using UnityEngine.UI;
using DG.Tweening;

namespace Core.ECS.Systems.Camera
{
    public class CameraFadeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraFadeEventComponent> _event = null;
        private readonly RawImage _fade;

        public CameraFadeSystem(RawImage fadeImage)
        {
            _fade = fadeImage;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _event)
            {
                ref float fadeTime = ref _event.Get1(i).FadeTime;
                float alpha = _event.Get1(i).FadeMode == FadeMode.On ? 1f : 0f;
                _fade.DOFade(alpha, fadeTime);
            }
        }
    }
}

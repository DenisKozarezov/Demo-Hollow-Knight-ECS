using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Entitas;
using Core.UI;

namespace Core.ECS.Behaviours
{
    public sealed class CameraBehaviour : EntityBehaviour, IAnyCameraFadeListener, IAnyCameraShakeListener
    {
        [SerializeReference]
        private Volume _volume;
        [SerializeField]
        private Fader _fader;

        private void Start()
        {
            Entity.AddVignette(_volume.profile.GetPostProcessSetting<Vignette>());

            RegisterListeners(Entity);
        }
        public void RegisterListeners(IEntity entity)
        {
            Entity.AddAnyCameraFadeListener(this);
            Entity.AddAnyCameraShakeListener(this);
        }
        public void UnregisterListeners()
        {
            Entity.RemoveAnyCameraFadeListener(this);
            Entity.RemoveAnyCameraShakeListener(this);
        }

        public void OnAnyCameraFade(GameEntity entity, float fadeTime, FadeMode fadeMode)
        {
            _fader.Fade(fadeMode, fadeTime);
        }
        public void OnAnyCameraShake(GameEntity entity, float shakeDuration, float shakeForce)
        {
            StartCoroutine(ShakeCoroutine(shakeDuration, shakeForce));
        }

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = transform.localPosition;
            while (elapsedTime < duration)
            {
                transform.localPosition = startPosition + Random.insideUnitSphere * magnitude;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = startPosition;
        }
    }
}

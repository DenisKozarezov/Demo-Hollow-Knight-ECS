using UnityEngine;
using System.Collections;
using Core.ECS.Events;
using Leopotam.Ecs;
using TMPro;

namespace Core.ECS.Systems
{
    internal class InteractSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private const float FadeTime = 0.5f;
        private const string PromptPath = "Prefabs/UI/Interactable Prompt";

        private Coroutine _coroutine;
        private SpriteRenderer _renderer;
        private TextMeshPro _text;

        private GameObject CreatePrompt(Transform target, Vector2 localPosition)
        {
            var asset = Resources.Load<GameObject>(PromptPath);
            var go = GameObject.Instantiate(asset, target);
            go.transform.localPosition = localPosition;
            return go;
        }
        public void Run()
        {
            foreach (var i in _enter)
            {
                ref var entity = ref _enter.GetEntity(i);
                ref var view = ref _enter.Get1(i);
                ShowLabel(ref view, FadeTime);
                entity.Destroy();
            }
            foreach (var i in _exit)
            {
                ref var entity = ref _exit.GetEntity(i);
                ref var view = ref _exit.Get1(i);
                HideLabel(ref view, FadeTime);
                entity.Destroy();
            }
        }
        private void HideLabel(ref InteractableTriggerExitEvent component, float fadeTime)
        {
            _coroutine = component.View.StartCoroutine(Fade(_renderer, _text, FadeMode.Off, fadeTime));
        }
        private void ShowLabel(ref InteractableTriggerEnterEvent component, float fadeTime)
        {      
            var prompt = CreatePrompt(component.View.transform, component.LocalPosition);

            _renderer = prompt.GetComponentInChildren<SpriteRenderer>();
            _renderer.color = _renderer.color.SetAlpha(0f);
            _text = prompt.GetComponentInChildren<TextMeshPro>();
            _text.color = _text.color.SetAlpha(0f);
            _text.text = component.View.InteractableLabel;
            _coroutine = component.View.StartCoroutine(Fade(_renderer, _text, FadeMode.On, fadeTime));
        }
        private IEnumerator Fade(SpriteRenderer renderer, TextMeshPro text, FadeMode mode, float fadeTime)
        {
            if (_coroutine != null) yield return _coroutine;    

            float elapsedTime = 0f;
            Color startColor = renderer.color;
            Color endColor = startColor.SetAlpha(mode == FadeMode.On ? 1f : 0f);
            while (elapsedTime < fadeTime)
            {
                if (renderer == null || text == null) yield return null;

                renderer.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
                text.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (mode == FadeMode.Off) GameObject.DestroyImmediate(renderer.gameObject);
            _coroutine = null;
            yield break;
        }
    }
}

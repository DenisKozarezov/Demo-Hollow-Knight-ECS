using UnityEngine;
using Core.ECS.Events;
using Leopotam.Ecs;
using TMPro;
using DG.Tweening;

namespace Core.ECS.Systems.UI
{
    internal class InteractablePromptUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private const float FadeTime = 0.5f;
        private const string PromptPath = "Prefabs/UI/Interactable Prompt";

        private Sequence _sequence;
        private SpriteRenderer _renderer;
        private TextMeshPro _text;
        private bool IsPlaying => _sequence.IsActive() && _sequence.IsPlaying();

        private GameObject CreatePrompt(Transform target, float offsetY)
        {
            var asset = Resources.Load<GameObject>(PromptPath);
            var go = GameObject.Instantiate(asset, target.position, Quaternion.identity);
            go.transform.position += Vector3.up * offsetY;
            return go;
        }
        public void Run()
        {
            foreach (var i in _enter)
            {
                ref var entity = ref _enter.GetEntity(i);
                ref var view = ref _enter.Get1(i);
                ShowLabel(ref view);
                entity.Destroy();
            }
            foreach (var i in _exit)
            {
                ref var entity = ref _exit.GetEntity(i);
                ref var view = ref _exit.Get1(i);
                HideLabel(ref view);
                entity.Destroy();
            }
        }
        private void HideLabel(ref InteractableTriggerExitEvent component)
        {
            Fade(FadeMode.Off);
        }
        private void ShowLabel(ref InteractableTriggerEnterEvent component)
        {
            if (!IsPlaying)
            {
                var prompt = CreatePrompt(component.View.transform, component.OffsetY);

                _renderer = prompt.GetComponentInChildren<SpriteRenderer>();
                _renderer.color = _renderer.color.SetAlpha(0f);
                _text = prompt.GetComponentInChildren<TextMeshPro>();
                _text.text = component.View.InteractableLabel;
                _text.color = _text.color.SetAlpha(0f);
            }
            Fade(FadeMode.On);
        }
        private void Fade(FadeMode mode)
        {
            if (IsPlaying) _sequence.Kill();

            float alpha = mode == FadeMode.On ? 1f : 0f;

            _sequence = DOTween.Sequence();
            _sequence.Join(_renderer.DOColor(_renderer.color.SetAlpha(alpha), FadeTime));
            _sequence.Join(_text.DOColor(_renderer.color.SetAlpha(alpha), FadeTime));
            _sequence.OnComplete(() =>
            {
                if (mode == FadeMode.Off) GameObject.DestroyImmediate(_renderer.gameObject);
            });
        }
    }
}

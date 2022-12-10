using UnityEngine;
using Core.ECS.Events;
using Leopotam.Ecs;
using TMPro;
using DG.Tweening;

namespace Core.ECS.Systems.UI
{
    public class InteractablePromptUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private const float FadeTime = 0.5f;
        private const string PromptPath = "Prefabs/UI/Interactable Prompt";

        private Sequence _sequence;
        private SpriteRenderer _renderer;
        private TextMeshPro _text;
        private bool IsPlaying => _sequence.IsActive() && _sequence.IsPlaying();

        private GameObject CreatePrompt(Vector2 position, string label)
        {
            var asset = Resources.Load<GameObject>(PromptPath);
            GameObject go = GameObject.Instantiate(asset, position, Quaternion.identity);
            _text = go.GetComponentInChildren<TextMeshPro>();
            _text.text = label;
            _text.color = _text.color.WithAlpha(0f);

            _renderer = go.GetComponentInChildren<SpriteRenderer>();
            _renderer.color = _renderer.color.WithAlpha(0f);
            return go;
        }
        void IEcsRunSystem.Run()
        {
            foreach (var i in _enter)
            {
                ref var entity = ref _enter.GetEntity(i);
                ref var @event = ref _enter.Get1(i);

                Vector2 position = @event.Position + Vector2.up * @event.InteractableComponent.OffsetY;
                ref string label = ref @event.InteractableComponent.Label;

                ShowLabel(ref position, ref label);
                entity.Destroy();
            }
            foreach (var i in _exit)
            {
                ref var entity = ref _exit.GetEntity(i);
                HideLabel();
                entity.Destroy();
            }
        }
        private void HideLabel()
        {
            Fade(FadeMode.Off);
        }
        private void ShowLabel(ref Vector2 position, ref string label)
        {
            if (!IsPlaying)
            {
                CreatePrompt(position, label);
            }
            Fade(FadeMode.On);
        }
        private void Fade(FadeMode mode)
        {
            if (IsPlaying) _sequence.Kill();

            float alpha = mode == FadeMode.On ? 1f : 0f;

            _sequence = DOTween.Sequence();
            _sequence.Join(_renderer.DOColor(_renderer.color.WithAlpha(alpha), FadeTime));
            _sequence.Join(_text.DOColor(_renderer.color.WithAlpha(alpha), FadeTime));
            _sequence.OnComplete(() =>
            {
                if (mode == FadeMode.Off) GameObject.DestroyImmediate(_renderer.gameObject);
            });
        }
    }
}

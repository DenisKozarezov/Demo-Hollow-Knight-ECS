using UnityEngine;
using Core.ECS.Events;
using Leopotam.Ecs;
using TMPro;
using DG.Tweening;

namespace Core.ECS.Systems.UI
{
    public sealed class InteractablePromptUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
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
                ref var entity = ref _enter.Get1(i);
                Vector2 position = entity.Position + Vector2.up * entity.InteractableComponent.OffsetY;
                ref string label = ref entity.InteractableComponent.Label;
                ShowLabel(ref position, ref label);
            }
            foreach (var i in _exit)
            {
                HideLabel();
            }
        }
        private void HideLabel() => Fade(FadeMode.Off, 0.5f);
        private void ShowLabel(ref Vector2 position, ref string label)
        {
            if (!IsPlaying)
            {
                CreatePrompt(position, label);
            }
            Fade(FadeMode.On, 0.5f);
        }
        private void Fade(FadeMode mode, float time)
        {
            if (IsPlaying) _sequence.Kill();

            float alpha = mode == FadeMode.On ? 1f : 0f;

            _sequence = DOTween.Sequence();
            _sequence.Join(_renderer.DOColor(_renderer.color.WithAlpha(alpha), time));
            _sequence.Join(_text.DOColor(_renderer.color.WithAlpha(alpha), time));
            _sequence.OnComplete(() =>
            {
                if (mode == FadeMode.Off) GameObject.DestroyImmediate(_renderer.gameObject);
            });
        }
    }
}

using UnityEngine;
using TMPro;
using DG.Tweening;
using Core.ECS;

namespace Core.UI
{
    public sealed class InteractablePromptBehaviour : EntityBehaviour
    {
        [SerializeField]
        private TextMeshPro _text;
        [SerializeField]
        private SpriteRenderer _renderer;
        private Sequence _sequence;

        public bool IsPlaying => _sequence.IsActive() && _sequence.IsPlaying();

        protected override void Start()
        {
            Entity.AddInteractablePrompt(this);
            _text.color = _text.color.WithAlpha(0f);
            _renderer.color = _text.color.WithAlpha(0f);
            transform
                .DOBlendableLocalMoveBy(Vector2.up * 0.15f, 2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject);
        }
        public void Fade(FadeMode mode, float time)
        {
            if (IsPlaying) _sequence.Kill();

            float alpha = mode == FadeMode.On ? 1f : 0f;

            _sequence = DOTween.Sequence();
            _sequence.Join(_renderer.DOColor(_renderer.color.WithAlpha(alpha), time));
            _sequence.Join(_text.DOColor(_renderer.color.WithAlpha(alpha), time));
            _sequence.OnComplete(() =>
            {
                if (mode == FadeMode.Off) GameObject.DestroyImmediate(gameObject);
            });
            _sequence.SetLink(gameObject);
        }
        public void SetText(string label) => _text.text = label;
    }
}
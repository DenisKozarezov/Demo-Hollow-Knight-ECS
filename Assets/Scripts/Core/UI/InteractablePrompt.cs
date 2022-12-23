using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Core.UI
{
    public class InteractablePrompt : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _text;
        [SerializeField]
        private SpriteRenderer _renderer;
        private Sequence _sequence;

        public bool IsPlaying => _sequence.IsActive() && _sequence.IsPlaying();

        private void Start()
        {
            _text.color = _text.color.WithAlpha(0f);
            _renderer.color = _text.color.WithAlpha(0f);
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
        }
        public void SetText(string label)
        {
            _text.text = label;
        }
    }
}
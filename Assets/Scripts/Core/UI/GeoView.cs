using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Core.UI
{
    public class GeoView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _geoCurrentText;
        [SerializeField]
        private TextMeshProUGUI _geoAddingText;
        [SerializeField]
        private Image _image;

        private int _currentValue;
        private int _addingValue;
        private Sequence _sequence;
        private bool _fading;
        private const float Duration = 5f;
        private const float AppearenceTime = 2f;

        private bool IsPlaying => _sequence.IsActive() && _sequence.IsPlaying();

        private void Start()
        {
            SetActive(false);
            SetColor(_geoCurrentText.color.SetAlpha(0f), _geoAddingText.color.SetAlpha(0f));
        }
        private void SetActive(bool isActive)
        {
            _geoCurrentText.gameObject.SetActive(isActive);
            _geoAddingText.gameObject.SetActive(isActive);
            _image.gameObject.SetActive(isActive);
        }
        private void SetColor(Color currentColor, Color addingColor)
        {
            _geoCurrentText.color = currentColor;
            _image.color = currentColor;
            _geoAddingText.color = addingColor;
        }
        private void SetAddingValue(int value)
        {
            _addingValue = value;
            _geoAddingText.text = $"{(value >= 0 ? "+" : "-")}{value}";
        }
        private void SetCurrentValue(int value)
        {
            _currentValue = value;
            _geoCurrentText.text = value.ToString();
        }
        public void AddValue(int value)
        {
            if (IsPlaying) _sequence.Kill();

            SetAddingValue(_addingValue + value);
            ShowGeo();
        }
        public void ReduceValue(int value)
        {
            if (IsPlaying) _sequence.Kill();

            SetAddingValue(_addingValue - value);
            ShowGeo();
        }    
        private void ShowGeo()
        {
            _sequence = DOTween.Sequence();
            if (!_fading) _sequence.Join(Fade(FadeMode.On));
            _sequence = _sequence.Append(GeoSequence());
            _sequence.Append(Fade(FadeMode.Off));
            _sequence.OnKill(() => _fading = false);
        }
        private Sequence Fade(FadeMode mode)
        {
            _fading = true;
            float alpha = mode == FadeMode.On ? 1f : 0f;

            SetActive(true);

            var sequence = DOTween.Sequence();
            sequence.Join(_image.DOColor(_image.color.SetAlpha(alpha), AppearenceTime));
            sequence.Join(_geoCurrentText.DOColor(_geoCurrentText.color.SetAlpha(alpha), AppearenceTime));
            sequence.Join(_geoAddingText.DOColor(_geoAddingText.color.SetAlpha(alpha), AppearenceTime));
            sequence.OnComplete(() =>
            {
                _fading = false;
                if (mode == FadeMode.Off) SetActive(false);
            });
            return sequence;
        }
        private Sequence GeoSequence()
        {
            int startCurrentValue = _currentValue;

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1f);
            sequence.Append(DOTween.To(() => _currentValue, x => SetCurrentValue(x), startCurrentValue + _addingValue, 1f));
            sequence.Join(DOTween.To(() => _addingValue, x => SetAddingValue(x), 0, 1f));
            sequence.AppendInterval(Duration);
            return sequence;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Core.UI
{
    public class GeoUIView : MonoBehaviour
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
        private Sequence _geoSequence;
        private bool _fading;
        private bool _paused;
        private float _timer;
        private const float Duration = 5f;
        private const float AppearenceTime = 2f;
        private bool IsPlaying => _sequence != null && _sequence.IsActive();

        private void Start()
        {
            SetActive(false);
            SetColor(_geoCurrentText.color.SetAlpha(0f), _geoAddingText.color.SetAlpha(0f));
        }
        private void Update()
        {
            if (_sequence == null || !_paused) return;

            if (_timer < AppearenceTime) _timer += Time.deltaTime;
            else
            {
                _paused = false;
                _timer = 0f;
                StartSequence();
            }
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
            SetAddingValue(_addingValue + value);
            ShowGeo();
        }
        public void ReduceValue(int value)
        {
            SetAddingValue(_addingValue - value);
            ShowGeo();
        }    
        private void ShowGeo()
        {
            if (!_fading) Fade(FadeMode.On);

            if (IsPlaying)
            {
                _geoSequence.Kill();
                _sequence.Kill();
                _paused = true;
            }
            if (_paused)
            {
                _timer = 0f;
                return;
            }
            StartSequence();
        }
        private void StartSequence()
        {
            _sequence = DOTween.Sequence();           
            _sequence.Append(GeoSequence());
            _sequence.Append(Fade(FadeMode.Off));
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

            _geoSequence = DOTween.Sequence();
            _geoSequence.Append(DOTween.To(() => _currentValue, x => SetCurrentValue(x), startCurrentValue + _addingValue, 1f));
            _geoSequence.Join(DOTween.To(() => _addingValue, x => SetAddingValue(x), 0, 1f));
            _geoSequence.AppendInterval(Duration);
            return _geoSequence;
        }
    }
}
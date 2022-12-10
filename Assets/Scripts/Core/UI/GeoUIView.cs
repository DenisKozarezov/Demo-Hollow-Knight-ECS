using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Core.UI
{
    public class GeoUIView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private TextMeshProUGUI _geoCurrentText;
        [SerializeField]
        private TextMeshProUGUI _geoAddingText;

        private int _currentValue;
        private int _addingValue;
        private Sequence _sequence;
        private const float Duration = 5f;
        private const float AppearenceTime = 2f;

        private void Start()
        {
            _canvasGroup.alpha = 0f;
            SetActive(false);
        }
        private void SetActive(bool isActive) => gameObject.SetActive(isActive);
        private void SetAddingValue(int signedValue)
        {
            _addingValue = signedValue;
            _geoAddingText.text = $"{(signedValue >= 0 ? "+" : "-")}{signedValue}";
        }
        private void SetCurrentValue(int value)
        {
            _currentValue = value;
            _geoCurrentText.text = value.ToString();
        }
        private Tweener Fade(FadeMode mode, float time)
        {
            if (mode == FadeMode.On) SetActive(true);

            float alpha = mode == FadeMode.On ? 1f : 0f;
            return DOTween.To(() => 1f - alpha, x => _canvasGroup.alpha = x, alpha, time)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    if (mode == FadeMode.Off) SetActive(false);
                });
        }
        private Sequence GeoSequence()
        {
            int startCurrentValue = _currentValue;

            Sequence geoSequence = DOTween.Sequence();
            geoSequence.Append(DOTween.To(() => _currentValue, x => SetCurrentValue(x), startCurrentValue + _addingValue, 1f));
            geoSequence.Join(DOTween.To(() => _addingValue, x => SetAddingValue(x), 0, 1f));
            geoSequence.AppendInterval(Duration);
            return geoSequence;
        }
        private void StartSequence()
        {
            if (!_sequence.IsActive()) 
                Fade(FadeMode.On, AppearenceTime);         
            else  
                _sequence.Kill();
            
            _sequence = GeoSequence();
            _sequence.PrependInterval(AppearenceTime);
            _sequence.Append(Fade(FadeMode.Off, AppearenceTime));
        }
        public void AddValue(int value)
        {
            SetAddingValue(_addingValue + value);
            StartSequence();
        }
        public void ReduceValue(int value)
        {
            SetAddingValue(_addingValue - value);
            StartSequence();
        }    
    }
}
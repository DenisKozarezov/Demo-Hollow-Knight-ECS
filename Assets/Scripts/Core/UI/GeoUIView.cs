using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Core.UI
{
    public class GeoUIView : UIBaseView
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

        private void Start()
        {
            _canvasGroup.alpha = 0f;
            SetActive(false);
        }
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
                Fade(_canvasGroup, FadeMode.On);         
            else  
                _sequence.Kill();
            
            _sequence = GeoSequence();
            _sequence.PrependInterval(2f);
            _sequence.Append(Fade(_canvasGroup, FadeMode.Off));
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
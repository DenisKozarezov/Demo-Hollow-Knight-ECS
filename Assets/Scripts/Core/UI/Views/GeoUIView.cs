using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Core.ECS.Behaviours
{
    public sealed class GeoUIView : UIBaseView
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private TextMeshProUGUI _geoCurrentText;
        [SerializeField]
        private TextMeshProUGUI _geoAddingText;

        private Sequence _sequence;
        private const float Duration = 5f;

        private bool IsPlaying => _sequence != null && _sequence.IsPlaying();
        private ref int CurrentGeo => ref Entity.currentGeo.Value;
        private ref int AddingGeo => ref Entity.addingGeo.Value;

        private void Start()
        {
            Entity.AddGeoUI(this);
            Entity.AddCurrentGeo(0);
            Entity.AddAddingGeo(0);

            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
        private Sequence GeoSequence()
        {
            int startCurrentValue = CurrentGeo;

            Sequence geoSequence = DOTween.Sequence();
            geoSequence.PrependInterval(2f);
            geoSequence.Append(DOTween.To(() => CurrentGeo, x => SetCurrentValue(x), startCurrentValue + AddingGeo, 1f));
            geoSequence.Join(DOTween.To(() => AddingGeo, x => SetAddingValue(x), 0, 1f));
            geoSequence.AppendInterval(Duration);
            geoSequence.Append(Fade(_canvasGroup, FadeMode.Off));
            return geoSequence;
        }
        private void SetAddingValue(int signedValue)
        {
            AddingGeo = signedValue;
            _geoAddingText.text = $"{(signedValue >= 0 ? "+" : "-")}{signedValue}";
        }
        private void SetCurrentValue(int value)
        {
            CurrentGeo = value;
            _geoCurrentText.text = value.ToString();
        }
        public void StartSequence(int currentValue, int addingValue)
        {
            if (!IsPlaying)
            {
                SetCurrentValue(currentValue);
                Fade(_canvasGroup, FadeMode.On);
            }
            else
            {
                SetAddingValue(AddingGeo + addingValue);
                _sequence.Kill();
            }
            _sequence = GeoSequence();
        }
    }
}
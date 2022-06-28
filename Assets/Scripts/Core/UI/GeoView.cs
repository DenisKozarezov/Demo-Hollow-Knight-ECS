using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core.UI
{
    public class GeoView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _geoText;
        [SerializeField]
        private Image _image; 
        private int _currentValue;
        private Coroutine _coroutine;
        private bool _fading;
        private const float AnnouncementDuration = 5f;
        private const float AnnouncementAppearenceTime = 2f;

        private void Start()
        {
            _geoText.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
            _geoText.color = _geoText.color.SetAlpha(0f);
            _image.color = _geoText.color.SetAlpha(0f);
        }
        public void AddValue(int value)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _currentValue += value;

            if (!_fading) StartCoroutine(Fade(FadeMode.On));
            _coroutine = StartCoroutine(GeoCoroutine(value, true));
            StartCoroutine(WaitCoroutine());
        }
        public void ReduceValue(int value)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _currentValue = Math.Max(_currentValue - value, 0);

            if (!_fading) StartCoroutine(Fade(FadeMode.On));
            _coroutine = StartCoroutine(GeoCoroutine(value, false));
            StartCoroutine(WaitCoroutine());
        }     
        private IEnumerator GeoCoroutine(int value, bool add)
        {
            int startValue = _currentValue;
            int endValue = add ? startValue + value : startValue - value;
            float elapsedTime = 0f;
            while (elapsedTime <= AnnouncementAppearenceTime)
            {
                int lerp = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, elapsedTime / AnnouncementAppearenceTime));
                _geoText.text = lerp.ToString();
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        private IEnumerator Fade(FadeMode mode)
        {
            _fading = true;

            _geoText.gameObject.SetActive(true);
            _image.gameObject.SetActive(true);

            float elapsedTime = 0f;
            Color startColor = _geoText.color;
            Color endColor = startColor.SetAlpha(mode == FadeMode.Off ? 0f : 1f);
            while (elapsedTime < AnnouncementAppearenceTime)
            {
                _geoText.color = Color.Lerp(startColor, endColor, elapsedTime / AnnouncementAppearenceTime);
                _image.color = Color.Lerp(startColor, endColor, elapsedTime / AnnouncementAppearenceTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (mode == FadeMode.Off)
            {
                _geoText.gameObject.SetActive(false);
                _image.gameObject.SetActive(false);
            }
            _fading = false;
        }
        private IEnumerator WaitCoroutine()
        {
            yield return _coroutine;
            yield return new WaitForSeconds(AnnouncementDuration);
            yield return Fade(FadeMode.Off);
        }
    }
}
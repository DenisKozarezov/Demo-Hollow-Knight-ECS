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
        private TextMeshProUGUI _geoCurrentText;
        [SerializeField]
        private TextMeshProUGUI _geoAddingText;
        [SerializeField]
        private Image _image;

        private int _currentValue;
        private int _addingValue;
        private Coroutine _coroutine;
        private bool _fading;
        private const float AnnouncementDuration = 5f;
        private const float AnnouncementAppearenceTime = 2f;

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
            _geoAddingText.text = $"+{value}";
        }
        private void SetCurrentValue(int value)
        {
            _currentValue = value;
            _geoCurrentText.text = value.ToString();
        }
        public void AddValue(int value)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            SetAddingValue(_addingValue + value);

            if (!_fading) StartCoroutine(Fade(FadeMode.On));
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(GeoCoroutine());
        }
        public void ReduceValue(int value)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            SetAddingValue(_addingValue - value);

            if (!_fading) StartCoroutine(Fade(FadeMode.On));
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(GeoCoroutine());
        }     
        private IEnumerator GeoCoroutine()
        {
            yield return new WaitForSeconds(2f);

            int startCurrentValue = _currentValue;
            int startAddingValue = _addingValue;
            float elapsedTime = 0f;
            while (elapsedTime <= 1f)
            {
                SetCurrentValue(Mathf.RoundToInt(Mathf.Lerp(startCurrentValue, _currentValue + _addingValue, elapsedTime)));             
                SetAddingValue(Mathf.RoundToInt(Mathf.Lerp(startAddingValue, 0, elapsedTime)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(AnnouncementDuration);
            yield return Fade(FadeMode.Off);
        }
        private IEnumerator Fade(FadeMode mode)
        {
            _fading = true;

            SetActive(true);

            float elapsedTime = 0f;
            Color startCurrentColor = _geoCurrentText.color;
            Color startAddingColor = _geoAddingText.color;
            Color endCurrentColor = startCurrentColor.SetAlpha(mode == FadeMode.Off ? 0f : 1f);
            Color endAddingColor = startAddingColor.SetAlpha(mode == FadeMode.Off ? 0f : 1f);
            while (elapsedTime < AnnouncementAppearenceTime)
            {
                float factor = elapsedTime / AnnouncementAppearenceTime;
                Color currentLerpColor = Color.Lerp(startCurrentColor, endCurrentColor, factor);
                Color addingLerpColor = Color.Lerp(startAddingColor, endAddingColor, factor);
                SetColor(currentLerpColor, addingLerpColor);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (mode == FadeMode.Off) SetActive(false);
            _fading = false;
        }
    }
}
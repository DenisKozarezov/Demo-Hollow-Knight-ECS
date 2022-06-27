using System.Collections;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class GameView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _bossText;
        [SerializeField]
        private TextMeshProUGUI _locationText;
        [SerializeField]
        private TextMeshProUGUI _gameOptionText;

        private const float AnnouncementDuration = 5f;
        private const float AnnouncementAppearenceTime = 2f;

        public void AnnounceBoss(string message)
        {
            _bossText.text = message;
            _bossText.color = _bossText.color.SetAlpha(0f);
            _bossText.gameObject.SetActive(true);
            StartCoroutine(AnnounceCoroutine(_bossText));
        }
        public void AnnounceLocation(string message)
        {
            _locationText.text = message;
            _locationText.color = _locationText.color.SetAlpha(0f);
            _locationText.gameObject.SetActive(true);
            StartCoroutine(AnnounceCoroutine(_locationText));
        }
        public void AnnounceGameOption(string message)
        {
            _gameOptionText.text = message;
            _gameOptionText.color = _gameOptionText.color.SetAlpha(0f);
            _gameOptionText.gameObject.SetActive(true);
            StartCoroutine(AnnounceCoroutine(_gameOptionText));
        }
        private IEnumerator Fade(TextMeshProUGUI text, FadeMode mode)
        {
            float elapsedTime = 0f;
            Color startColor = text.color;
            Color endColor = startColor.SetAlpha(mode == FadeMode.On ? 1f : 0f);
            while (elapsedTime < AnnouncementAppearenceTime)
            {
                text.color = Color.Lerp(startColor, endColor, elapsedTime / AnnouncementAppearenceTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }          
        }
        private IEnumerator AnnounceCoroutine(TextMeshProUGUI text)
        {
            yield return Fade(text, FadeMode.On);
            yield return new WaitForSeconds(AnnouncementDuration);
            yield return Fade(text, FadeMode.Off);
            text.gameObject.SetActive(false);
        }
    }
}
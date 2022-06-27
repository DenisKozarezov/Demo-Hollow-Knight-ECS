using System.Collections;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class GameView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _announcementText;

        private const float AnnouncementDuration = 5f;
        private const float AnnouncementAppearenceTime = 2f;

        public void Announce(string message)
        {
            _announcementText.text = message;
            _announcementText.color = _announcementText.color.SetAlpha(0f);
            _announcementText.gameObject.SetActive(true);
            StartCoroutine(AnnounceCoroutine(AnnouncementDuration, AnnouncementAppearenceTime));
        }
        private IEnumerator Fade(FadeMode mode, float fadeTime)
        {
            float elapsedTime = 0f;
            Color startColor = _announcementText.color;
            Color endColor = startColor.SetAlpha(mode == FadeMode.On ? 1f : 0f);
            while (elapsedTime < fadeTime)
            {
                _announcementText.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }          
        }
        private IEnumerator AnnounceCoroutine(float duration, float appearenceTime)
        {
            yield return Fade(FadeMode.On, appearenceTime);
            yield return new WaitForSeconds(duration);
            yield return Fade(FadeMode.Off, appearenceTime);
            _announcementText.gameObject.SetActive(false);
        }
    }
}
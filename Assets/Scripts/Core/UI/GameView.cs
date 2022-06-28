using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Core.UI
{
    public class GameView : MonoBehaviour
    {
        public enum GameEventType : byte
        {
            GameSave = 0x00,
            MapUpdated = 0x01,
        }
        private Dictionary<GameEventType, string> _gameEvents = new Dictionary<GameEventType, string>
        {
            { GameEventType.GameSave, "Game saved" },
            { GameEventType.MapUpdated, "Map updated" }
        };

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _bossText;
        [SerializeField]
        private TextMeshProUGUI _locationText;
        [SerializeField]
        private TextMeshProUGUI _gameEventText;

        private const float AnnouncementDuration = 5f;
        private const float AnnouncementAppearenceTime = 2f;

        private void Start()
        {
            _bossText.gameObject.SetActive(false);
            _locationText.gameObject.SetActive(false);
            _gameEventText.gameObject.SetActive(false);
        }
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
        public void AnnounceGameMessage(GameEventType messageType)
        {
            _gameEventText.text = _gameEvents[messageType];
            _gameEventText.color = _gameEventText.color.SetAlpha(0f);
            _gameEventText.gameObject.SetActive(true);
            StartCoroutine(AnnounceCoroutine(_gameEventText));
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
            if (mode == FadeMode.Off) text.gameObject.SetActive(false);
        }
        private IEnumerator AnnounceCoroutine(TextMeshProUGUI text)
        {
            yield return Fade(text, FadeMode.On);
            yield return new WaitForSeconds(AnnouncementDuration);
            yield return Fade(text, FadeMode.Off);
        }
    }
}
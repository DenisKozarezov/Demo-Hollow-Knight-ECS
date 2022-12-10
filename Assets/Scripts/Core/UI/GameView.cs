using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
        [SerializeField] private TextMeshProUGUI _bossText;
        [SerializeField] private TextMeshProUGUI _locationText;
        [SerializeField] private TextMeshProUGUI _gameEventText;
        [SerializeField] private Image _bottomFleur, _topFleur;

        private const float AnnouncementDuration = 1f;
        private const float FadeTime = 1f;

        private void Start()
        {
            _bossText.gameObject.SetActive(false);
            _locationText.gameObject.SetActive(false);
            _gameEventText.gameObject.SetActive(false);
            _bottomFleur.gameObject.SetActive(false);
            _topFleur.gameObject.SetActive(false);
        }
        public void AnnounceBoss(string message)
        {
            _bossText.text = message;
            ShowFleurImage(_bottomFleur);
            ShowFleurImage(_topFleur);
            ShowText(_bossText);
        }
        public void AnnounceLocation(string message)
        {
            _locationText.text = message;
            ShowText(_locationText);
        }
        public void AnnounceGameMessage(GameEventType messageType)
        {
            _gameEventText.text = _gameEvents[messageType];
            ShowText(_gameEventText);
        }

        private void ShowFleurImage(Image image)
        {
            image.gameObject.SetActive(true);

            var sequence = DOTween.Sequence();
            sequence.Append(image.DOColor(image.color.WithAlpha(1f), FadeTime));
            sequence.AppendInterval(AnnouncementDuration);
            sequence.Append(image.DOColor(image.color.WithAlpha(0f), FadeTime));
            sequence.OnComplete(() =>
            {
                image.gameObject.SetActive(false);
            });
        }
        
        private void ShowText(TextMeshProUGUI text)
        {
            text.color = _gameEventText.color.WithAlpha(0f);
            text.gameObject.SetActive(true);

            var sequence = DOTween.Sequence();
            sequence.Append(text.DOColor(text.color.WithAlpha(1f), FadeTime));
            sequence.AppendInterval(AnnouncementDuration);
            sequence.Append(text.DOColor(text.color.WithAlpha(0f), FadeTime));
            sequence.OnComplete(() =>
            {
                text.gameObject.SetActive(false);
            });
        }
    }
}
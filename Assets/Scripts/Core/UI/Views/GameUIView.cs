using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class GameUIView : MonoBehaviour
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

        private void Start()
        {
            _locationText.gameObject.SetActive(false);
            _gameEventText.gameObject.SetActive(false);
        }
        public void AnnounceBoss(string message)
        {
            _bossText.text = message;
            _bossText.transform.parent.gameObject.SetActive(true);
        }
        public void AnnounceGameMessage(GameEventType messageType)
        {
            _gameEventText.text = _gameEvents[messageType];            
        }        
    }
}
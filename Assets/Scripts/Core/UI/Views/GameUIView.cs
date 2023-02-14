using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Entitas;
using Core.Models;

namespace Core.ECS.Behaviours
{
    public sealed class GameUIView : UIBaseView, IAnyEnteredBossZoneListener
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
            Entity.AddGameUI(this);
            _locationText.gameObject.SetActive(false);
            _gameEventText.gameObject.SetActive(false);
            RegisterListeners();
        }
        private void AnnounceBoss(string message)
        {
            _bossText.text = message;
            _bossText.transform.parent.gameObject.SetActive(true);
        }
        private void AnnounceGameMessage(GameEventType messageType)
        {
            _gameEventText.text = _gameEvents[messageType];            
        }

        public void RegisterListeners()
        {
            Entity.AddAnyEnteredBossZoneListener(this);
        }
        public void UnregisterListeners()
        {
            Entity.RemoveAnyEnteredBossZoneListener(this);
        }
        public void OnAnyEnteredBossZone(GameEntity entity, EnemyModel bossModel) => AnnounceBoss(bossModel.DisplayName);
    }
}
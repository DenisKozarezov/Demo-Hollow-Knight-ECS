using System;
using UnityEngine;
using UnityEngine.UI;
using Entitas;

namespace Core.ECS.Behaviours
{
    public sealed class HealthUIView : UIBaseView, IDamageTakenListener, IRestoredHealthListener
    {
        private GameEntity _entity;

        [SerializeField]
        private Transform _healthTransform;
        [SerializeField]
        private Sprite _emptyHealth;
        [SerializeField]
        private Sprite _fullHealth;
        [SerializeField]
        private GameObject _healthPrefab;

        private int _currentHealth;
        private int Count => _healthTransform.childCount;

        private void Start()
        {
            Clear();
            Entity.AddHealthUI(this);
        }

        public void Init(int value)
        {
            if (value == 0) return;

            _currentHealth = value;

            for (int i = 0; i < value; i++) Instantiate(_healthPrefab, _healthTransform);
        }
        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDamageTakenListener(this);
            _entity.AddRestoredHealthListener(this);
        }
        public void UnregisterListeners()
        {
            _entity.RemoveDamageTakenListener();
            _entity.RemoveRestoredHealthListener();
        }

        private void Hit(int value)
        {
            if (value == 0 || Count == 0 || _currentHealth == 0) return;

            for (int i = _currentHealth; i > _currentHealth - value && i > 0; i--)
            {
                var image = _healthTransform.GetChild(i - 1).GetComponentInChildren<Image>();
                image.sprite = _emptyHealth;
            }
            _currentHealth = Math.Max(_currentHealth - value, 0);
        }
        private void RestoreHealth(int value)
        {
            if (value == 0 || _currentHealth >= Count) return;

            for (int i = _currentHealth; i < _currentHealth + value && i < Count; i++)
            {
                var image = _healthTransform.GetChild(i).GetComponentInChildren<Image>();
                image.sprite = _fullHealth;
            }
            _currentHealth = Math.Min(_currentHealth + value, Count);
        }
        private void Clear()
        {
            _currentHealth = 0;
            for (int i = Count; i > 0; i--)
            {
                Destroy(_healthTransform.GetChild(i - 1).gameObject);
            }
        }

        // Player events
        public void OnDamageTaken(GameEntity entity, int value) => Hit(value);
        public void OnRestoredHealth(GameEntity entity, int value) => RestoreHealth(value);
    }
}

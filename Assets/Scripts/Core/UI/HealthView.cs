using UnityEngine;
using UnityEngine.UI;
using Editor;

namespace Core.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField]
        private Transform _healthTransform;
        [SerializeField]
        private Sprite _emptyHealth;
        [SerializeField]
        private Sprite _fullHealth;
        [SerializeField, ObjectPicker]
        private string _healthPrefab;
        private string _additiveHealthPrefab;

        private byte _currentHealth;
        private int Count => _healthTransform.childCount;

        public void Init(byte value, bool additiveHealth = false)
        {
            if (value == 0) return;

            _currentHealth = value;

            var asset = Resources.Load(additiveHealth ? _additiveHealthPrefab : _healthPrefab);
            for (byte i = 0; i < value; i++) Instantiate(asset, _healthTransform);
        }
        public void RestoreHealth(byte value)
        {
            if (value == 0) return;

            for (byte i = _currentHealth; i < _currentHealth + value; i++)
            {
                var image = _healthTransform.GetChild(i).GetComponentInChildren<Image>();
                image.sprite = _fullHealth;
            }
            _currentHealth += value;
        }
        public void Hit(byte value)
        {
            if (value == 0 || Count == 0 || _currentHealth == 0) return;

            for (byte i = _currentHealth; i > _currentHealth - value && i > 0; i--)
            {
                var image = _healthTransform.GetChild(i - 1).GetComponentInChildren<Image>();
                image.sprite = _emptyHealth;
            }
            _currentHealth -= value;
        }
        public void Clear()
        {
            _currentHealth = 0;
            for (int i = Count; i > 0; i--)
            {
                Destroy(_healthTransform.GetChild(i - 1).gameObject);
            }
        }
    }
}
using UnityEngine;
using Editor;

namespace Core.UI
{
    internal class HealthView : MonoBehaviour
    {
        [SerializeField]
        private Transform _healthTransform;
        [SerializeField, ObjectPicker]
        private string _healthPrefab;
        private string _additiveHealthPrefab;

        private byte Value => (byte)_healthTransform.childCount;

        public void AddHealth(byte value, bool additiveHealth = false)
        {
            if (value == 0) return;

            var asset = Resources.Load(additiveHealth ? _additiveHealthPrefab : _healthPrefab);
            for (byte i = 0; i < value; i++) Instantiate(asset, _healthTransform);
        }
        public void RemoveHealth(byte value)
        {
            if (value == 0 || Value == 0) return;

            // Damage is too high, clear all lifepoints
            if (value > Value)
            {
                Clear();
                return;
            }

            // Reduce some lifepoints
            for (byte i = Value; i > Value - value; i--)
            {
                Destroy(_healthTransform.GetChild(i - 1).gameObject);
            }
        }
        public void Clear()
        {
            for (byte i = Value; i > 0; i--)
            {
                Destroy(_healthTransform.GetChild(i - 1).gameObject);
            }
        }
    }
}
using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Create Healing Focus")]
    public class HealingFocusAbility : AbilityModel
    {
        [SerializeField, Min(0)]
        private int _healthRestore;
        [SerializeField, Min(0f)]
        private float _holdTime;
        [SerializeField, Space, ObjectPicker]
        private string _effectPath;

        public int HealthRestore => _healthRestore;
        public float HoldTime => _holdTime;
        public string EffectPath => _effectPath;
    }
}
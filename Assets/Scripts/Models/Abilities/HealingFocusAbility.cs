using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Create HealingFocus")]
    public class HealingFocusAbility : AbilityModel
    {
        [SerializeField]
        private byte _healthRestore;
        [SerializeField, Min(0f)]
        private float _holdTime;

        public byte HealthRestore => _healthRestore;
        public float HoldTime => _holdTime;
    }
}
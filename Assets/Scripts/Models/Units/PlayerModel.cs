using System.Collections.Generic;
using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Model")]
    public sealed class PlayerModel : UnitModel
    {
        [SerializeField, MinMaxSlider(0f, 25f)]
        private Vector2 _jumpForceRange;
        [SerializeField, Min(0f)]
        private float _attackCooldown;
        [SerializeField, Min(0f)]
        private float _hitEnergyRestore;

        [Space, SerializeField]
        private List<string> _abilities;
        
        public Vector2 JumpForceRange => _jumpForceRange;
        public float AttackCooldown => _attackCooldown;
        public float HitEnergyRestore => _hitEnergyRestore;
        public const float EnergyCapacity = 100f;
    }
}
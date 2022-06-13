using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Model")]
    public sealed class PlayerModel : UnitModel
    {
        [SerializeField, Min(0f)]
        private float _attackCooldown;
        [SerializeField, Min(0f)]
        private float _jumpForce;
        [SerializeField, Min(0f)]
        private float _hitEnergyRestore;

        [Space, SerializeField]
        private List<string> _abilities;
        
        public float AttackCooldown => _attackCooldown;
        public float JumpForce => _jumpForce;
        public float HitEnergyRestore => _hitEnergyRestore;
        public const float EnergyCapacity = 100f;
    }
}
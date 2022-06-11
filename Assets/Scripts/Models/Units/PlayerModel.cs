using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Model")]
    public class PlayerModel : UnitModel
    {
        [SerializeField, Min(0f)]
        private float _attackCooldown;
        [SerializeField, Min(0f)]
        private float _jumpForce;

        [Space, SerializeField]
        private List<string> _abilities;
        
        public float AttackCooldown => _attackCooldown;
        public float JumpForce => _jumpForce;
    }
}
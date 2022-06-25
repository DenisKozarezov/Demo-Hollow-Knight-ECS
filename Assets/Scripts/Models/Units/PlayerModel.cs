using System.Collections.Generic;
using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Model")]
    public sealed class PlayerModel : UnitModel
    {
        [SerializeField, MinMaxSlider(0f, 25f)]
        private Vector2 _jumpHeightRange;
        [SerializeField, Min(0f)]
        private float _attackCooldown;
        [SerializeField, Min(0f)]
        private float _hitEnergyRestore;

        [Space, SerializeField]
        private List<AbilityModel> _abilities;
        
        public Vector2 JumpHeightRange => _jumpHeightRange;
        public float AttackCooldown => _attackCooldown;
        public float HitEnergyRestore => _hitEnergyRestore;
        public float EnergyCapacity => 100f;

        public T GetAbility<T>() where T : AbilityModel
        {
            var ability = _abilities.Find(x => x is T) as T;
            if (ability == null)
            {
                Debug.LogWarning($"Player has no such ability <b><color=yellow>{typeof(T).Name}</color></b>.");
            }
            return ability;
        }
    }
}
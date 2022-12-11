using System.Collections.Generic;
using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Model")]
    public sealed class PlayerModel : UnitModel
    {
        [field: SerializeField, MinMaxSlider(0f, 25f)] public Vector2 JumpHeightRange { get; private set; }
        [field: SerializeField, Min(0f)] public float AttackCooldown { get; private set; }
        [field: SerializeField, Min(0f)] public float HitEnergyRestore { get; private set; }

        [Space, SerializeField]
        private List<AbilityModel> _abilities;

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
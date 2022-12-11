using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Create Healing Focus")]
    public class HealingFocusAbility : AbilityModel
    {
        [field: SerializeField, Min(0)] public int HealthRestore { get; private set; }
        [field: SerializeField, Min(0f)] public float HoldTime { get; private set; }
        [field: SerializeField, Space, ObjectPicker] public string EffectPath { get; private set; }
    }
}
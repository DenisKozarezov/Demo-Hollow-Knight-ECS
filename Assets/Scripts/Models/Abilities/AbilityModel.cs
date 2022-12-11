using System;
using UnityEngine;

namespace Core.Models
{
    public abstract class AbilityModel : ScriptableObject, IEquatable<AbilityModel>
    {
        [field: Header("Common Characteristics")]
        [field: SerializeField] public uint ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Header("Ability Characteristics")]
        [field: SerializeField, Min(0f)] public float EnergyCost { get; private set; }

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(DisplayName))
            {
                DisplayName = name;
            }
        }

        public bool Equals(AbilityModel other)
        {
            if (other == null) return false;
            return ID == other.ID;
        }
    }
}
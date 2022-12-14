using System;
using UnityEngine;
using Editor;

namespace Core.Models
{
    public abstract class UnitModel : ScriptableObject, IEquatable<UnitModel>
    {
        [field: Header("Common Characteristics")]
        [field: SerializeField] public uint ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: Space, SerializeField, ObjectPicker] public string PrefabPath { get; private set; }

        [field: Header("Unit Characteristics")]
        [field: SerializeField, Min(0f)] public float MovementSpeed { get; private set; }
        [field: SerializeField, Min(0)] public int BaseDamage { get; private set; }
        [field: SerializeField, Range(0f, 5f)] public float AttackRange { get; private set; }
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(DisplayName))
            {
                DisplayName = name;
            }
        }

        public bool Equals(UnitModel other)
        {
            if (other is null) return false;
            return ID == other.ID;
        }
    }
}
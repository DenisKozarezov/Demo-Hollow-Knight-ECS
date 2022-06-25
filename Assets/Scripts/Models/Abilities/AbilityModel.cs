using System;
using UnityEngine;
using Editor;

namespace Core.Models
{
    public abstract class AbilityModel : ScriptableObject, IEquatable<AbilityModel>
    {
        [Header("Common Characteristics")]
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;

        [Space, SerializeField]
        private Sprite _icon;

        [Header("Ability Characteristics")]
        [SerializeField, Min(0f)]
        private float _energyCost;

        public uint ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float EnergyCost => _energyCost;

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(_displayName))
            {
                _displayName = name;
            }
        }

        public bool Equals(AbilityModel other)
        {
            return _id == other._id;
        }
    }
}
using System;
using UnityEngine;
using Editor;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Unit Model")]
    public class UnitModel : ScriptableObject, IEquatable<UnitModel>
    {
        [Header("Common Characteristics")]
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;

        [Space, SerializeField, ObjectPicker]
        private string _prefabPath;

        public uint ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public string PrefabPath => _prefabPath;  

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(_displayName))
            {
                _displayName = name;
            }
        }

        public bool Equals(UnitModel other)
        {
            return _id == other._id;
        }
    }
}
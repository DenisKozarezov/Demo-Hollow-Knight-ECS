using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField, Range(0f, 1f)]
        private float _globalVolume;
        [SerializeField, Range(0f, 1f)]
        private float _musicVolume;
        [SerializeField, Range(0f, 1f)]
        private float _environmentVolume;
        [SerializeField]
        private bool _isMute;

        public float GlobalVolume => _globalVolume;
        public float MusicVolume => _musicVolume;
        public float EnvironmentVolume => _environmentVolume;
        public bool IsMute => _isMute;
    }
}
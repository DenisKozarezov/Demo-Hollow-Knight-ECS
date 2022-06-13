using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Create UnitsDefinitions")]
    public sealed class UnitsDefinitions : ScriptableObject
    {
        [Header("Units")]
        [SerializeField]
        private PlayerModel _playerModel;
        [SerializeField]
        private UnitModel _falseKnight;

        public PlayerModel PlayerModel => _playerModel;
        public UnitModel FalseKnight => _falseKnight;
    }
}
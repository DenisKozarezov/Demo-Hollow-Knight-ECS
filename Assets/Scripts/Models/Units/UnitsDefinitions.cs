using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Create Units Definitions")]
    public sealed class UnitsDefinitions : ScriptableObject
    {
        [Header("Units")]
        [SerializeField]
        private PlayerModel _playerModel;
        [SerializeField]
        private FalseKnightModel _falseKnight;

        public PlayerModel PlayerModel => _playerModel;
        public FalseKnightModel FalseKnight => _falseKnight;
    }
}
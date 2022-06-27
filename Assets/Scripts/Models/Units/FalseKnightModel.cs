using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create False Knight Model")]
    public class FalseKnightModel : UnitModel
    {
        [SerializeField, Min(0f)]
        private float _jumpHeight;

        public float JumpHeight => _jumpHeight;
    }
}
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create False Knight Model")]
    public class FalseKnightModel : UnitModel
    {
        [field: SerializeField, Min(0f)] public float JumpHeight { get; private set; }
    }
}
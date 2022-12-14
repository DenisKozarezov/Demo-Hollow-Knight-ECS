using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create False Knight Model")]
    public class FalseKnightModel : EnemyModel
    {
        [field: SerializeField, Min(0f)] public float JumpHeight { get; private set; }
    }
}
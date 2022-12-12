using UnityEngine;

namespace Core.Models
{
    public abstract class EnemyModel : UnitModel
    {
        [field: SerializeField] public ushort GeoReward { get; private set; }
    }
}
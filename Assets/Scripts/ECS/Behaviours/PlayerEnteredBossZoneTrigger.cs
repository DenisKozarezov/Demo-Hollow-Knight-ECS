using UnityEngine;
using Core.Models;

namespace Core.ECS.Behaviours
{
    public sealed class PlayerEnteredBossZoneTrigger : CollisionEntityBehaviour
    {
        [SerializeField]
        private EnemyModel _boss;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (TriggerBy(other, out GameEntity entered))
            {
                entered.AddEnteredBossZone(_boss);
                Entity.isDestroyed = true;
            }
        }
    }
}

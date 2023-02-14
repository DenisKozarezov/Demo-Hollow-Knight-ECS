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
            if (!other.Matches(_triggeringLayers)) return;

            Game.collisionRegistry.Value
                       .Take(other.GetInstanceID())?
                       .With(x => x.Entity?.With(e => e.AddEnteredBossZone(_boss)));

            Entity.isDestroyed = true;
        }
    }
}

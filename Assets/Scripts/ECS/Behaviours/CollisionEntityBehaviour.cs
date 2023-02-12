using UnityEngine;

namespace Core.ECS.Behaviours
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEntityBehaviour : EntityBehaviour
    {
        [SerializeField]
        private LayerMask _triggeringLayers;

        protected virtual void OnTriggerEnter2D(Collider2D other) => TriggerBy(other);
        protected virtual void OnTriggerStay2D(Collider2D other) => TriggerBy(other);
        protected virtual void OnTriggerExit2D(Collider2D other) => ResetTriggerBy(other);
        protected virtual void OnCollisionEnter2D(Collision2D other) => TriggerBy(other.collider);
        protected virtual void OnCollisionExit2D(Collision2D other) => ResetTriggerBy(other.collider);

        private void TriggerBy(Collider2D collision)
        {
            if (Entity.hasCollided) return;

            if (collision.Matches(_triggeringLayers))
            {
                GameEntity entered = Game
                  .collisionRegistry.Value
                  .Take(collision.GetInstanceID())
                  .Entity;

                Entity?.ReplaceCollided(entered.id.Value);
                entered?.ReplaceCollided(Entity.id.Value);
            }
        }
        private void ResetTriggerBy(Collider2D collision)
        {
            if (!Entity.hasCollided) return;

            if (collision.Matches(_triggeringLayers))
            {
                GameEntity exit = Game
                  .collisionRegistry.Value
                  .Take(collision.GetInstanceID())
                  .Entity;

                if (exit?.id.Value == Entity.collided.CollidedID)
                {
                    Entity.With(x => x.RemoveCollided(), Entity.hasCollided);
                    exit.With(x => x.RemoveCollided(), exit.hasCollided);
                }
            }
        }
    }
}
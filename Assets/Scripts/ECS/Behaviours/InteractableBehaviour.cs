using UnityEngine;
using Core.ECS;

namespace Core.UI
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class InteractableBehaviour : EntityBehaviour
    {
        [SerializeField]
        private LayerMask _triggeringLayers;

        private void OnTriggerEnter2D(Collider2D other) => TriggerBy(other);
        private void OnTriggerStay2D(Collider2D other) => TriggerBy(other);
        private void OnTriggerExit2D(Collider2D other) => ResetTriggerBy(other);
        protected override void Start()
        {
            Entity.isInteractable = true;
        }

        private void TriggerBy(Collider2D collision)
        {
            if (!Entity.isInteractable || Entity.hasCollided) return;

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
                    Entity.RemoveCollided();
                    exit.RemoveCollided();
                }
            }
        }
    }
}
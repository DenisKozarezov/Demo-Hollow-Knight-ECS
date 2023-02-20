using UnityEngine;

namespace Core.ECS.Behaviours
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEntityBehaviour : EntityBehaviour
    {
        [SerializeField]
        protected LayerMask _triggeringLayers;

        protected virtual void OnTriggerEnter2D(Collider2D other) 
            => TriggerBy(other, out GameEntity _);
        protected virtual void OnTriggerExit2D(Collider2D other) 
            => ResetTriggerBy(other, out GameEntity _);
        protected virtual void OnCollisionEnter2D(Collision2D other) 
            => TriggerBy(other.collider, out GameEntity _);
        protected virtual void OnCollisionExit2D(Collision2D other) 
            => ResetTriggerBy(other.collider, out GameEntity _);

        protected bool TriggerBy(Collider2D collision, out GameEntity entered)
        {
            entered = null;

            if (collision.Matches(_triggeringLayers))
            {
                entered = Game
                  .collisionRegistry.Value
                  .Take(collision.GetInstanceID())
                  .Entity;

                Entity?.ReplaceCollided(entered.id.Value);
                entered?.ReplaceCollided(Entity.id.Value);
                return true;
            }
            return false;
        }
        protected bool ResetTriggerBy(Collider2D collision, out GameEntity exit)
        {
            exit = null;

            if (collision.Matches(_triggeringLayers))
            {
                exit = Game
                  .collisionRegistry.Value
                  .Take(collision.GetInstanceID())
                  .Entity;

                Entity.With(x => x.RemoveCollided(), Entity.hasCollided);
                exit.With(x => x.RemoveCollided(), exit.hasCollided);
                return true;
            }
            return false;
        }
    }
}
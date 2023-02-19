using UnityEngine;

namespace Core.ECS.Behaviours
{
    public sealed class GroundBehaviour : CollisionEntityBehaviour
    {
        protected override void OnTriggerEnter2D(Collider2D collider) => MarkGrounded(collider);
        protected override void OnTriggerExit2D(Collider2D collider) => UnmarkGrounded(collider);
        protected override void OnCollisionEnter2D(Collision2D other) => MarkGrounded(other.collider);
        protected override void OnCollisionExit2D(Collision2D other) => UnmarkGrounded(other.collider);
        private void OnTriggerStay2D(Collider2D collider) => MarkGrounded(collider);

        private void MarkGrounded(Collider2D collider)
        {
            if (TriggerBy(collider, out GameEntity entered))
            {
                entered.isGrounded = true;
            }
        }
        private void UnmarkGrounded(Collider2D collider)
        {
            if (ResetTriggerBy(collider, out GameEntity exit))
            {
                exit.isGrounded = false;
            }
        }
    }
}

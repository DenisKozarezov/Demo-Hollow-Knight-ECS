using UnityEngine;

namespace Core.ECS.Behaviours
{
    public sealed class GroundBehaviour : EntityBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider) => MarkGrounded(collider);
        private void OnTriggerExit2D(Collider2D collider) => UnmarkGrounded(collider);
        private void OnCollisionEnter2D(Collision2D other) => MarkGrounded(other.collider);
        private void OnCollisionExit2D(Collision2D other) => UnmarkGrounded(other.collider);
        private void OnTriggerStay2D(Collider2D collider) => MarkGrounded(collider);

        private void MarkGrounded(Collider2D collider)
        {
            Game.collisionRegistry.Value
                      .Take(collider.GetInstanceID())?
                      .With(x => x.Entity?.With(e => e.isGrounded = true));
        }
        private void UnmarkGrounded(Collider2D collider)
        {
            Game.collisionRegistry.Value
                .Take(collider.GetInstanceID())?
                .With(x => x.Entity?.With(e => e.isGrounded = false));
        }
    }
}

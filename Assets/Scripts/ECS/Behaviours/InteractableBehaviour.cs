using UnityEngine;

namespace Core.ECS.Behaviours
{
    public sealed class InteractableBehaviour : CollisionEntityBehaviour
    {
        [SerializeField]
        private string _label;
        [SerializeField]
        private InteractType _interactType;

        private void Start() => Entity.AddInteractable(_label, _interactType);
        protected override void OnTriggerEnter2D(Collider2D collider) => MarkCanInteract(collider);
        protected override void OnTriggerExit2D(Collider2D collider) => UnmarkCanInteract(collider);

        private void MarkCanInteract(Collider2D collider)
        {
            if (TriggerBy(collider, out GameEntity entered))
            {
                Entity.isCanInteract = true;
                entered.isCanInteract = true;
            }
        }
        private void UnmarkCanInteract(Collider2D collider)
        {
            if (ResetTriggerBy(collider, out GameEntity exit))
            {
                Entity.isCanInteract = false;
                exit.isCanInteract = false;
            }
        }
    }
}
using UnityEngine;
using Core.ECS.Events;
using Voody.UniLeo;

namespace Core.UI
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableView : MonoBehaviour
    {
        [SerializeField]
        private string _interactableLabel;
        [SerializeField]
        private float _offsetY;
        [SerializeField]
        private InteractType _interactionType;
        
        public string InteractableLabel => _interactableLabel;
        public InteractType InteractionType => _interactionType;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity(new InteractableTriggerEnterEvent
                {
                    View = this,
                    OffsetY = _offsetY
                });
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity(new InteractableTriggerExitEvent
                {
                    View = this
                });
            }
        }
    }
}
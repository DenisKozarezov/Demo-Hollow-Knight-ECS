using UnityEngine;
using Core.ECS.Events;
using Voody.UniLeo;

namespace Core.UI
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableView : MonoBehaviour
    {
        [SerializeField]
        private bool _interactable = true;
        [Space, SerializeField]
        private string _label;
        [SerializeField]
        private float _offsetY;
        [SerializeField]
        private InteractType _interactionType;

        public bool Interactable => _interactable;
        public string Label => _label;
        public InteractType InteractionType => _interactionType;

        public void SetInteractable(bool isInteractable)
        {
            _interactable = isInteractable;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_interactable) return;

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
            if (!_interactable) return;

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
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
        private InteractType _interactType;
        private float _height;
        
        public string InteractableLabel => _interactableLabel;
        public InteractType InteractType => _interactType;

        private void Start()
        {
            var collider = GetComponent<Collider2D>();
            _height = collider.bounds.size.y;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity(new InteractableTriggerEnterEvent
                {
                    View = this,
                    LocalPosition = Vector2.up * _height
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
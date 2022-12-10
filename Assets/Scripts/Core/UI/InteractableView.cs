using UnityEngine;
using Core.ECS.Events;
using Voody.UniLeo;
using AI.ECS;
using Core.ECS.Components;
using Leopotam.Ecs;

namespace Core.UI
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(EntityReference))]
    public class InteractableView : MonoBehaviour
    {
        [SerializeField]
        private EntityReference _entityReference;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity(new InteractableTriggerEnterEvent
                {
                    InteractableEntity = _entityReference.Entity,
                    InteractableComponent = _entityReference.Entity.Get<InteractableComponent>(),
                    Position = transform.position
                });
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity<InteractableTriggerExitEvent>();
            }
        }
    }
}
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;
using Core.ECS.Components.Units;

namespace Core.Units
{
    public class UnitScript : MonoBehaviour
    {
        private EntityReference _entityReference;
        public EntityReference EntityReference => _entityReference;

        private void Start()
        {
            _entityReference = GetComponent<EntityReference>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_entityReference.Entity.IsAlive() || _entityReference.Entity.IsNull()) return;

            if (collision.collider.gameObject.layer == Constants.GroundLayer)
            {
                foreach (var contact in collision.contacts)
                {
                    if (contact.normal == Vector2.up)
                    {
                        float dx = collision.otherCollider.bounds.size.x * 0.5f;
                        Vector2 point = contact.point + Vector2.right * dx;
                        _entityReference.Entity.Get<OnGroundComponent>().Point = point;
                        break;
                    }
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (_entityReference.Entity.IsNullOrEmpty()) return;

            if (collision.collider.gameObject.layer == Constants.GroundLayer)
            {
                _entityReference.Entity.Del<OnGroundComponent>();
            }
        }
    }
}
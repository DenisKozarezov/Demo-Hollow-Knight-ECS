using System.Collections;
using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DestroyEntitiesSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent, SpriteRendererComponent, DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                Collider2D collider = _filter.Get1(i).Value;
                SpriteRenderer renderer = _filter.Get2(i).Value;
                collider.attachedRigidbody.simulated = false;
                collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                collider.enabled = false;
                collider.gameObject.layer = Constants.IgnoreRaycastLayer;
                SetColorToWhite(renderer);
                entity.Destroy();
            }
        }
        private void SetColorToWhite(SpriteRenderer renderer)
        {
            renderer.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ResetColorCoroutine(renderer, 1f));
        }
        private IEnumerator ResetColorCoroutine(SpriteRenderer renderer, float fadeTime)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                renderer.color = Color.Lerp(Color.white, Color.black, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
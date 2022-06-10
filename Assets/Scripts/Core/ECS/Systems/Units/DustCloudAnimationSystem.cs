using UnityEngine;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems
{
    internal sealed class DustCloudAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimateDustEventComponent> _filter = null;

        private float TimeAliveSeconds = 0.7f;

        private GameObject _prefabDustAnimation;

        public DustCloudAnimationSystem(GameObject prefabDustAnimation)
        {
            _prefabDustAnimation = prefabDustAnimation;
        }      

        private GameObject InstantiatePrefab(ref AnimateDustEventComponent dustComponent)
        {
            GameObject prefab = GameObject.Instantiate(_prefabDustAnimation);
            prefab.transform.position = dustComponent.Point;
            prefab.transform.rotation = Quaternion.identity;
            prefab.transform.localScale = dustComponent.Scale;
            dustComponent.DustAnimation = prefab;
            return prefab;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var dustComponent = ref entity.Get<AnimateDustEventComponent>();

                // Instantiate dust effect
                dustComponent.DustAnimation ??= InstantiatePrefab(ref dustComponent);

                // Effect lifetime
                dustComponent.TimeAlive += Time.deltaTime;   
                if (dustComponent.TimeAlive > TimeAliveSeconds)
                {
                    DestroyPrefab(dustComponent.DustAnimation, ref entity);
                }                   
            }
        }

        private static void DestroyPrefab(GameObject prefab, ref EcsEntity ecsEntity)
        {
            GameObject.Destroy(prefab);
            ecsEntity.Del<AnimateDustEventComponent>();
        }
    }
}
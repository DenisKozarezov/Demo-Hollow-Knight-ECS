using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Units
{
    internal class EnemyDroppingGeoSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsFilter<ColliderComponent, EnemyComponent, DiedComponent> _filter = null;

        private ObjectPool<ItemView> Pool;
        private const string GeoPrefabPath = "Prefabs/Items/Geo";
        private const float Force = 15f;

        private Vector3 GetRandomForce()
        {
            float randomAngle = Random.Range(50f, 80f);
            return Vector2.up.RotateVector(randomAngle).normalized * Force;
        }

        public void Init()
        {
            Pool = new ObjectPool<ItemView>(GeoPrefabPath, 50);
        }
        public void Destroy()
        {
            Pool.Dispose();
        }
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                for (int j = 0; j < 30; j++)
                {
                    var geo = Pool.Get();
                    geo.SetPool(Pool);

                    var rigidbody = (geo as ItemView).GetComponent<Rigidbody2D>();                    
                    if (!rigidbody.gameObject.activeInHierarchy) rigidbody.gameObject.SetActive(true);

                    rigidbody.transform.position = collider.Value.transform.position;
                    rigidbody.velocity = GetRandomForce();
                }
            }
        }
    }
}

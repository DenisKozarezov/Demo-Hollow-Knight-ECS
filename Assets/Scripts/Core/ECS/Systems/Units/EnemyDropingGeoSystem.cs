using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Units
{
    internal class EnemyDroppingGeoSystem : IEcsRunSystem
    {
        private EcsFilter<ColliderComponent, EnemyComponent, DiedComponent> _filter = null;

        private const string GeoPrefabPath = "Prefabs/Items/Geo";
        private const float Force = 10f;

        private Rigidbody2D CreatePrefab(Vector2 position)
        {
            var asset = Resources.Load(GeoPrefabPath) as GameObject;
            var prefab = GameObject.Instantiate(asset, position, Quaternion.identity);
            return prefab.GetComponent<Rigidbody2D>();
        }
        private Vector3 GetRandomForce()
        {
            float randomAngle = Random.Range(50f, 80f);
            return Vector2.up.RotateVector(randomAngle).normalized * Force;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                for (int j = 0; j < 100; j++)
                {
                    var geo = CreatePrefab(collider.Value.transform.position);
                    geo.velocity = GetRandomForce();
                }
            }
        }
    }
}

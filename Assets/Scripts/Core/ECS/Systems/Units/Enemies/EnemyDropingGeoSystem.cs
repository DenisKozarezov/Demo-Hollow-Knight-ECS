using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Units
{
    public class EnemyDroppingGeoSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<ColliderComponent, EnemyComponent, DiedComponent> _filter = null;
        private readonly GeoView.Factory _factory;

        public EnemyDroppingGeoSystem(GeoView.Factory factory)
        {
            _factory = factory;
        }

        private Vector3 GetRandomForce(float force)
        {
            float randomAngle = Random.Range(50f, 80f);
            return Vector2.up.RotateVector(randomAngle).normalized * force;
        }

        public void Destroy()
        {
            _factory.Dispose();
        }
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                for (int j = 0; j < 30; j++)
                {
                    GeoView geo = _factory.Create();
                    Rigidbody2D rigidbody = geo.GetComponent<Rigidbody2D>();
                    rigidbody.transform.position = collider.Value.transform.position;
                    rigidbody.velocity = GetRandomForce(15f);
                }
            }
        }
    }
}

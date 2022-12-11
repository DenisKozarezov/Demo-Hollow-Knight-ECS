using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems.Units
{
    public class EnemyDroppingGeoSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
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

        private void OnPlayerObtainedGeo(GeoView geo)
        {
            geo.Obtained -= OnPlayerObtainedGeo;
            geo.Dispose();           
            _world.NewEntity(new PlayerObtainedGeoEvent { Value = 3 });
        }
        void IEcsDestroySystem.Destroy() => _factory.Dispose();
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                for (int j = 0; j < 30; j++)
                {
                    GeoView geo = _factory.Create();
                    geo.Obtained += OnPlayerObtainedGeo;
                    geo.transform.position = collider.Value.transform.position;
                    geo.SetVelocity(GetRandomForce(15f));
                }
            }
        }
    }
}

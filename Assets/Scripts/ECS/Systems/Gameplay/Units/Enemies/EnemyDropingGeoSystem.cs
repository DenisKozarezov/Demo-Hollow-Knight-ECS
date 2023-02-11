using UnityEngine;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Units
{
    public class EnemyDroppingGeoSystem/* : IEcsRunSystem*/
    {
        //private readonly EcsWorld _world = null;
        //private readonly EcsFilter<TransformComponent, EnemyComponent, DiedComponent> _enemies = null;
        //private readonly GeoView.Factory _factory;

        //public EnemyDroppingGeoSystem(GeoView.Factory factory)
        //{
        //    _factory = factory;
        //}

        //private Vector3 GetRandomForce(float force)
        //{
        //    float randomAngle = Random.Range(50f, 80f);
        //    return Vector2.up.RotateVector(randomAngle).normalized * force;
        //}

        //private void OnPlayerObtainedGeo(GeoView geo)
        //{
        //    geo.Obtained -= OnPlayerObtainedGeo;
        //    geo.Dispose();           
        //    _world.NewEntity(new PlayerObtainedGeoEvent { Value = 2 });
        //}
        //void IEcsRunSystem.Run()
        //{
        //    foreach (var i in _enemies)
        //    {
        //        Vector2 position = _enemies.Get1(i).Value.position;
        //        ushort geoReward = _enemies.Get2(i).EnemyModel.GeoReward;

        //        for (int j = 0; j < geoReward / 2; j++)
        //        {
        //            GeoView geo = _factory.Create();
        //            geo.Obtained += OnPlayerObtainedGeo;
        //            geo.transform.position = position;
        //            geo.SetVelocity(GetRandomForce(15f));
        //        }
        //    }
        //}
    }
}

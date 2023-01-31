using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;

namespace Core.ECS.Systems
{
    public sealed class CreateDustCloudSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CreateDustEventComponent> _filter = null;
        private const string PrefabPath = "Prefabs/VFX/Smoke/Dust";

        private GameObject InstantiatePrefab(ref Vector2 point, ref Vector3 scale)
        {
            var asset = Resources.Load(PrefabPath) as GameObject;
            GameObject prefab = GameObject.Instantiate(asset, point, Quaternion.identity);
            prefab.transform.localScale = scale;
            return prefab;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                GameObject.Destroy(InstantiatePrefab(ref component.Point, ref component.Scale), 1f);
            }
        }
    }
}
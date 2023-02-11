using UnityEngine;
using Core.ECS.Components.Units;
using DG.Tweening;

namespace Core.ECS.Systems
{
    public sealed class EntitiesCleanupSystem/* : IEcsRunSystem*/
    {
        //private readonly EcsFilter<ColliderComponent, SpriteRendererComponent, DiedComponent> _filter = null;

        //void IEcsRunSystem.Run()
        //{
        //    foreach (var i in _filter)
        //    {
        //        ref var entity = ref _filter.GetEntity(i);
        //        SpriteRenderer renderer = _filter.Get2(i).Value;
        //        renderer.DOColor(Color.black, 1f);
                
        //        entity.Destroy();
        //    }
        //}
    }
}
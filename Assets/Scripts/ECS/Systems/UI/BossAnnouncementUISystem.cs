using Core.UI;

namespace Core.ECS.Systems.UI
{
    public sealed class BossAnnouncementUISystem /*: IEcsRunSystem*/
    {
        //private readonly EcsFilter<PlayerEnteredBossZoneEvent> _event = null;
        //private readonly GameUIView _view;

        //public BossAnnouncementUISystem(GameUIView view)
        //{
        //    _view = view;
        //}

        //void IEcsRunSystem.Run()
        //{
        //    foreach (var i in _event)
        //    {
        //        ref var entity = ref _event.GetEntity(i);
        //        string displayName = _event.Get1(i).BossModel.DisplayName;
        //        _view.AnnounceBoss(displayName);
        //        entity.Destroy();
        //    }
        //}
    }
}

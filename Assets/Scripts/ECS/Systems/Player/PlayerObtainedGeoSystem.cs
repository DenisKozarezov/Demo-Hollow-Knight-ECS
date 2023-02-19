using Core.ECS.Behaviours;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerObtainedGeoSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _geos;
        private readonly IGroup<GameEntity> _UIViews;
        private readonly IGroup<GameEntity> _players;

        public PlayerObtainedGeoSystem(GameContext game)
        {
            _geos = game.GetGroup(GameMatcher.AllOf(GameMatcher.Geo, GameMatcher.Collided));
            _UIViews = game.GetGroup(GameMatcher.GeoUI);
            _players = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player, GameMatcher.CurrentGeo)
                .NoneOf(GameMatcher.Dead));
        }
        public void Execute()
        {
            foreach (GameEntity player in _players)
            {
                foreach (GameEntity uiView in _UIViews)
                {
                    foreach (GameEntity geo in _geos)
                    {
                        int currentGeo = player.currentGeo.Value;
                        ushort obtainedGeo = geo.geo.Value;
                        int newGeo = currentGeo + obtainedGeo;

                        player.ReplaceCurrentGeo(newGeo);
                        geo.isDestroyed = true;

                        GeoUIView view = uiView.geoUI.Value;
                        view.StartSequence(currentGeo, obtainedGeo);
                    }
                }
            }            
        }
    }
}

using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerObtainedGeoSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _geos;
        private readonly IGroup<GameEntity> _players;

        public PlayerObtainedGeoSystem(GameContext game)
        {
            _geos = game.GetGroup(GameMatcher.AllOf(GameMatcher.Geo, GameMatcher.Collided));
            _players = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player, GameMatcher.CurrentGeo)
                .NoneOf(GameMatcher.Dead));
        }
        public void Execute()
        {
            foreach (GameEntity player in _players)
            {
                foreach (GameEntity entity in _geos)
                {
                    ushort obtainedGeo = entity.geo.Value;
                    int newGeo = player.currentGeo.Value + obtainedGeo;
                    player.ReplaceCurrentGeo(newGeo);
                    player.ReplaceObtainedGeo(obtainedGeo);
                    entity.isDestroyed = true;
                }           
            }
        }
    }
}

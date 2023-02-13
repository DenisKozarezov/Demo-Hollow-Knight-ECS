using System.Collections.Generic;
using Entitas;
using Core.ECS.Behaviours;

namespace Core.ECS.Systems.Units
{
    public sealed class EnemyDroppingGeoSystem : ReactiveSystem<GameEntity>
    {
        private readonly GeoView.Factory _factory;

        public EnemyDroppingGeoSystem(GameContext game, GeoView.Factory factory) : base(game)
        {
            _factory = factory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Dead.Added());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasEnemy && entity.hasCollider;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity deadEnemy in entities)
            {
                ushort geoReward = deadEnemy.enemy.Value.GeoReward;

                for (int i = 0; i < geoReward / 2; i++)
                {
                    GeoView geo = _factory.Create(2);
                    geo.transform.position = deadEnemy.collider.Value.bounds.center;
                }
            }
        }    
    }
}

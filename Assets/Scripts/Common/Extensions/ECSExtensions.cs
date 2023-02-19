using System.Linq;
using Entitas;

namespace Core
{
    public static class ECSExtensions
    {
        public static GameContext Game() => Contexts.sharedInstance.game;
        public static GameEntity Empty() => Game().CreateEntity();
        public static TEntity Duplicate<TEntity>(this TEntity entity) where TEntity : class, IEntity
        {
            TEntity duplicate = entity.Context().CreateEntity();
            entity.CopyTo(duplicate);

            return duplicate;
        }
        public static IContext<TEntity> Context<TEntity>(this TEntity entity) where TEntity : class, IEntity
        {
            return Contexts.sharedInstance.allContexts
                .FirstOrDefault(c => entity.contextInfo.name == c.contextInfo.name) 
                as IContext<TEntity>;
        }
    }
}

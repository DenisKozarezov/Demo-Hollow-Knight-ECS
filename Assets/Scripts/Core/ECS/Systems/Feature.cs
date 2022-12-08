using Leopotam.Ecs;

namespace Core.ECS.Systems
{
    public abstract class Feature : IEcsSystem
    {
        protected readonly GameContext GameContext;

        public Feature(GameContext context)
        {
            GameContext = context;
        }

        protected void Add(IEcsSystem system)
        {
            GameContext.EcsSystems.Add(system);
        }
        protected void OneFrame<T>() where T : struct
        {
            GameContext.EcsSystems.OneFrame<T>();
        }
        public void Init()
        {
            GameContext.EcsSystems.Init();
        }
        public void Run()
        {
            GameContext.EcsSystems?.Run();
        }
        public void Destroy()
        {
            GameContext.EcsSystems.Destroy();
        }
    }
}

namespace Core.ECS.Systems.Units
{
    public sealed class UnitsSystems : Feature
    {
        public UnitsSystems(Contexts contexts) : base(nameof(UnitsSystems))
        {
            Add(new FallingSystem(contexts.game));
            Add(new GroundedSystem(contexts.game));
            Add(new CreateDustCloudSystem(contexts.game));
        }
    }
}

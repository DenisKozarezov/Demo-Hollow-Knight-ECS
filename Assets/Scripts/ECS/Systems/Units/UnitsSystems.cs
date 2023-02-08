using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Systems.FalseKnight;
using Core.Models;

namespace Core.ECS.Systems.Units
{
    public sealed class UnitsSystems : Feature
    {
        public UnitsSystems(GameContext context) : base(context)
        { 
            Add(new EntityInitSystem());
            Add(new FalseKnightInitSystem(context.UnitsModelsProvider.Resolve<FalseKnightModel>()));
            Add(new FalseKnightJumpAnimationSystem());
            Add(new FalseKnightAttackAnimationSystem());
            Add(new CreateDustCloudSystem());
            Add(new DamageAnimationSystem());

            OneFrame<EntityInitComponent>();
            OneFrame<CreateUnitEventComponent>();
            OneFrame<CreateDustEventComponent>();
            OneFrame<DiedComponent>();
        }
    }
}

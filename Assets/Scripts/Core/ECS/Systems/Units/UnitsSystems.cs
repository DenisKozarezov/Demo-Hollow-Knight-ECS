using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Systems.FalseKnight;

namespace Core.ECS.Systems.Units
{
    internal class UnitsSystems : Feature
    {
        internal UnitsSystems(GameContext context) : base(context)
        {
            Add(new UnitInitSystem());
            Add(new FalseKnightInitSystem(context.UnitsDefinitions.FalseKnight));
            Add(new FalseKnightJumpAnimationSystem());
            Add(new FalseKnightAttackAnimationSystem());
            Add(new DustCloudAnimationSystem());
            Add(new DamageAnimationSystem());

            OneFrame<UnitInitComponent>();
            OneFrame<UnitCreateEventComponent>();
            OneFrame<DiedComponent>();
        }
    }
}

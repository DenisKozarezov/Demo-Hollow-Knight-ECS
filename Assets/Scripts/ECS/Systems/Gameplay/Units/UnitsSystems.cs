using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Systems.FalseKnight;
using Core.Models;

namespace Core.ECS.Systems.Units
{
    public sealed class UnitsSystems : Feature
    {
        public UnitsSystems(Contexts contexts) : base(nameof(UnitsSystems))
        { 
            //Add(new EntityInitSystem());
            //Add(new FalseKnightInitSystem(context.UnitsModelsProvider.Resolve<FalseKnightModel>()));
            //Add(new FalseKnightJumpAnimationSystem());
            //Add(new FalseKnightAttackAnimationSystem());
            //Add(new CreateDustCloudSystem());
            //Add(new DamageAnimationSystem());
        }
    }
}

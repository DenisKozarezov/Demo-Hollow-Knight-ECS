﻿namespace Core.ECS.Systems.Units
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
            Add(new GroundedSystem(contexts.game));
        }
    }
}
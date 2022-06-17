using System;
using UnityEngine;
using Leopotam.Ecs;

namespace Core.ECS.Events.FalseKnight
{
    internal struct FalseKnightStrongAttackEvent : IEcsIgnoreInFilter { }
    internal struct FalseKnightJumpEvent : IEcsIgnoreInFilter { }
    internal struct FalseKnightRollEvent : IEcsIgnoreInFilter { }
    [Serializable] internal struct FalseKnightAttackEventComponent { public Animator Animator; }
}
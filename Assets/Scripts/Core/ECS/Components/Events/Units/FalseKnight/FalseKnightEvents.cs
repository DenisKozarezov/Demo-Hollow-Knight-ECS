using System;
using UnityEngine;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Events.FalseKnight
{
    internal struct FalseKnightStrongAttackEvent : IEcsIgnoreInFilter { }
    internal struct FalseKnightJumpEvent : IEcsIgnoreInFilter { }
    internal struct FalseKnightRollEvent : IEcsIgnoreInFilter { }
    [Serializable] internal struct FalseKnightAttackEventComponent { public Animator Animator; }
}
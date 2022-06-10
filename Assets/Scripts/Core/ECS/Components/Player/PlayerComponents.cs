using System;
using UnityEngine;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Components.Player
{    
    public struct PlayerTagComponent : IEcsIgnoreInFilter { }
    public struct PlayerJumpComponent : IEcsIgnoreInFilter {}
    public struct PlayerAttackComponent : IEcsIgnoreInFilter { }
}
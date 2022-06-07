using System;
using UnityEngine;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Components.Player
{    
    [Serializable] public struct PlayerMoveComponent { public GameObject GameObject; }
    public struct PlayerJumpComponent : IEcsIgnoreInFilter {}
    public struct PlayerAttackComponent : IEcsIgnoreInFilter { }
}
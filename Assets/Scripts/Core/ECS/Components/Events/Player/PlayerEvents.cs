using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.Models;

namespace Core.ECS.Events.Player
{
    public struct PlayerDiedEvent : IEcsIgnoreInFilter { }
    public struct PlayerObtainedGeoEvent { public int Value; }
    public struct PlayerRecievedDamageEvent { public int Value;  }
    public struct PlayerHealedEvent { public int Value; }
    public struct EnergyReducedEvent { public float Value; }
    public struct PlayerEnteredBossZoneEvent { public UnitModel BossModel; }
    public struct PlayerTalkingWithNPCEvent { public NPCComponent NPC; }
}

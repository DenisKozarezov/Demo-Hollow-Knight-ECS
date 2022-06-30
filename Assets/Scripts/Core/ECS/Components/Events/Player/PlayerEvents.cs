using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.Models;

namespace Core.ECS.Events.Player
{
    internal struct PlayerDiedEvent : IEcsIgnoreInFilter { }
    internal struct PlayerObtainedGeoEvent { public int Value; }
    internal struct PlayerRecievedDamageEvent { public int Value;  }
    internal struct PlayerHealedEvent { public int Value; }
    internal struct EnergyReducedEvent { public float Value; }
    internal struct PlayerEnteredBossZoneEvent { public UnitModel BossModel; }
    internal struct PlayerTalkingWithNPCEvent { public NPCComponent NPC; }
}

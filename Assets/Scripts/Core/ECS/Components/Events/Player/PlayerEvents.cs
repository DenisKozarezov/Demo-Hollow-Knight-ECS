using Core.Models;
using Leopotam.Ecs;

namespace Core.ECS.Events.Player
{
    internal struct PlayerDiedEvent : IEcsIgnoreInFilter { }
    internal struct PlayerRecievedDamageEvent { public int Value;  }
    internal struct PlayerHealedEvent { public int Value; }
    internal struct EnergyReducedEvent { public float Value; }
    internal struct PlayerEnteredBossZoneEvent { public UnitModel BossModel; }
}

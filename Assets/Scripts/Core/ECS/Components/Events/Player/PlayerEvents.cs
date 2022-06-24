using Leopotam.Ecs;

namespace Core.ECS.Events.Player
{
    internal struct PlayerRecievedDamageEvent { public byte Value;  }
    internal struct PlayerHealedEvent { public byte Value; }
    internal struct EnergyReducedEvent { public float Value; }
    internal struct PlayerDiedEvent : IEcsIgnoreInFilter { }
}

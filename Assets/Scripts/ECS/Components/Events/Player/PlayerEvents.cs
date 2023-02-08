using Core.ECS.Components.Units;
using Core.Models;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events.Player
{
    [Event(Self)] public sealed class PlayerDiedEvent : IComponent { }
    [Event(Self)] public sealed class PlayerObtainedGeoEvent : IComponent { public int Value; }
    [Event(Self)] public sealed class PlayerRecievedDamageEvent : IComponent { public int Value;  }
    [Event(Self)] public sealed class PlayerHealedEvent : IComponent { public int Value; }
    [Event(Self)] public sealed class EnergyReducedEvent : IComponent { public float Value; }
    [Event(Self)] public sealed class PlayerEnteredBossZoneEvent : IComponent { public EnemyModel BossModel; }
    [Event(Self)] public sealed class PlayerTalkingWithNPCEvent : IComponent { public NPCComponent NPC; }
}

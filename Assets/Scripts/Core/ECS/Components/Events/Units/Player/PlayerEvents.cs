using Leopotam.Ecs;

namespace Core.ECS.Events.Player
{
    internal struct PlayerRecievedDamageEvent : IEcsIgnoreInFilter {  }
    internal struct PlayerHealthModifiedEvent
    {
        public short Delta;
    }
}

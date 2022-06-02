using UnityEngine;

namespace Core.Units
{
    public interface IEnemy
    {
        bool Taunted { get; }
        Transform Target { get; }
        void Taunt(Transform target);
    }
}
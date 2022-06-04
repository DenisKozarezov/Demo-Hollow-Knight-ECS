using UnityEngine;

namespace Core.Units
{
    public class Zombie : UnitView, IEnemy
    {        
        public bool Taunted { get; set; }
        public Transform Target { get; set; }

        public void Taunt(Transform target)
        {
            Taunted = true;
            Target = target;
        }
    }
}
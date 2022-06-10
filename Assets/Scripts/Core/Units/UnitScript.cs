using UnityEngine;
using Examples.Example_1.ECS.ComponentProviders;

namespace Core.Units
{
    public class UnitScript : MonoBehaviour
    {
        private EntityReference _entityReference;

        private void Start()
        {
            _entityReference = GetComponent<EntityReference>();
        }
    }
}
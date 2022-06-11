using UnityEngine;
using AI.ECS;

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
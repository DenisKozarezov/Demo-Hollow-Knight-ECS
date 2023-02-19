using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    public interface IPhysicsService
    {
        IEnumerable<GameEntity> RaycastThroughScreenPoint(Vector2 position, int layerMask);
        IEnumerable<GameEntity> RaycastCircle(Vector2 position, float radius, int layerMask);
    }
}

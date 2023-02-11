using UnityEngine;

namespace Core
{
    public static class VectorExtensions
    {
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            Vector3 tmp = vector;
            tmp.x = x;
            vector = tmp;
            return vector;
        }
        public static Vector3 SetY(this Vector3 vector, float y)
        {
            Vector3 tmp = vector;
            tmp.y = y;
            vector = tmp;
            return vector;
        }
        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            Vector3 tmp = vector;
            tmp.z = z;
            vector = tmp;
            return vector;
        }
        public static Vector2 SetX(this Vector2 vector, float x)
        {
            Vector2 tmp = vector;
            tmp.x = x;
            vector = tmp;
            return vector;
        }
        public static Vector2 SetY(this Vector2 vector, float y)
        {
            Vector2 tmp = vector;
            tmp.y = y;
            vector = tmp;
            return vector;
        }
        public static Vector3 AddX(this Vector3 vector, float xDelta)
        {
            Vector3 tmp = vector;
            tmp.x = tmp.x + xDelta;
            vector = tmp;
            return vector;
        }
        public static Vector3 AddY(this Vector3 vector, float yDelta)
        {
            Vector3 tmp = vector;
            tmp.y = tmp.y + yDelta;
            vector = tmp;
            return vector;
        }
        public static Vector2 AddX(this Vector2 vector, float xDelta)
        {
            Vector2 tmp = vector;
            tmp.x = tmp.x + xDelta;
            vector = tmp;
            return vector;
        }
        public static Vector2 AddY(this Vector2 vector, float yDelta)
        {
            Vector2 tmp = vector;
            tmp.y = tmp.y + yDelta;
            vector = tmp;
            return vector;
        }
    }
}

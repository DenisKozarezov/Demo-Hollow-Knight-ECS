using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core
{
    public static class RenderingExtensions
    {
        public static T GetPostProcessSetting<T>(this VolumeProfile volume) where T : VolumeComponent
        {
            if (!volume.TryGet(out T setting))
            {
                throw new NullReferenceException($"There is no override <b><color=yellow>{typeof(T).Name}</color></b> in camera Global Volume.");
            }
            return setting;
        }
    }

    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }

    public static class MathExtensions
    {
        public static Vector2 RotateVector(this Vector2 vector, float angle)
        {
            float x = Mathf.Cos(angle) * vector.x - Mathf.Sin(angle) * vector.y;
            float y = Mathf.Sin(angle) * vector.x + Mathf.Cos(angle) * vector.y;
            return new Vector2(x, y);
        }
    }
}
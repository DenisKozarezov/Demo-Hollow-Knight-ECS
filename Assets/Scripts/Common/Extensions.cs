using System;
using UnityEngine;
using UnityEngine.Rendering;
using Leopotam.Ecs;

namespace Core
{
    public static class CameraExtensions
    {
        public static T GetPostProcessSetting<T>(this Camera camera) where T : VolumeComponent
        {
            var volume = camera.GetComponentInChildren<Volume>();
            if (!volume.profile.TryGet<T>(out var setting))
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

    public static class EcsExtensions
    {
        public static bool IsNullOrEmpty(this EcsEntity entity)
        {
            return !entity.IsAlive() || entity.IsNull() || !entity.IsWorldAlive();
        }
        public static T NewEntity<T>(this EcsWorld world) where T : struct
        {
            return world.NewEntity().Get<T>();
        }
        public static void NewEntity<T>(this EcsWorld world, in T value) where T : struct
        {
            world.NewEntity().Get<T>() = value;
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
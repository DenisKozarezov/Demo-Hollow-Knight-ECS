using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Core
{
    public static class CameraExtensions
    {
        public static T GetPostProcessSetting<T>(this Camera camera) where T : PostProcessEffectSettings
        {
            var volume = camera.GetComponentInChildren<PostProcessVolume>();
         
            if (volume.profile.TryGetSettings<T>(out var setting)) return setting;
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"There is no override <b><color=yellow>{typeof(T).Name}</color></b> in camera post-process volume. ");
#endif
                return null;
            }
        }
    }
}
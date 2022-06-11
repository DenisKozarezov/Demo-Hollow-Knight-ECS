using UnityEngine;

namespace Core.Models
{
    enum ScreenResolution : byte
    {
        _800x600 = 0,
        _1024x768 = 1,
        _1280x720 = 2,
        _1280x800 = 4,

    }

    [CreateAssetMenu(menuName = "Configuration/Settings/Create GraphicsSettings")]
    public class GraphicsSettings : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField]
        private ScreenResolution _screenResolution;
    }
}
using UnityEngine;

namespace Editor.BehaviorTreeEditor.Config
{
    public enum ThemeStyle
    {
        LightTheme,
        DarkTheme
    }
    [CreateAssetMenu()]
    public class StyleThemeConfig : ScriptableObject
    {
        public ThemeStyle Theme;
    }
}
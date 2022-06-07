using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class ToolbarMenuThemeStyle : ToolbarMenu
    {
        public new class UXMLFactory : UxmlFactory<ToolbarMenuThemeStyle, ToolbarMenu.UxmlTraits> { }
    }
}
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Editor
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
    public sealed class ProgressBarAttribute : PropertyAttribute
    {
        public ProgressBarAttribute()
        {

        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
    internal sealed class ProgressBarAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Float)
            {
                Debug.LogWarning("ProgressBarAttribute requires a Float property");
                return;
            }

            EditorGUI.LabelField(position, label);
            position.x += EditorGUIUtility.labelWidth;
            position.width = position.width - EditorGUIUtility.labelWidth;

            float progress = Mathf.Clamp01(property.floatValue);

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.ProgressBar(position, progress, $"{progress * 100}%");
            EditorGUI.EndProperty();
        }
    }
#endif
}
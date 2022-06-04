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
        public float MinValue { get; set; } = 0f;
        public float MaxValue { get; set; } = 1f;
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

            var attr = (ProgressBarAttribute)attribute;

            EditorGUI.LabelField(position, label);
            position.x += EditorGUIUtility.labelWidth;
            position.width = position.width - EditorGUIUtility.labelWidth;

            float progress = (property.floatValue - attr.MinValue) / (attr.MaxValue - attr.MinValue);

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.ProgressBar(position, progress, $"{progress * 100}%");
            EditorGUI.EndProperty();
        }
    }
#endif
}
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Editor
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public sealed class ObjectPickerAttribute : PropertyAttribute
    {
        public ObjectPickerAttribute()
        {

        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ObjectPickerAttribute))]
    internal sealed class ObjectPickerAttributeDrawer : PropertyDrawer
    {
        private const float ButtonWidth = 50f;
        private const string PathPrefix = "Assets/Resources/";

        private int _currentPickerWindow;     

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2.5f;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                Debug.LogWarning("ObjectPickerAttribute requires a String property");
                return;
            }

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));

            position.y += EditorGUIUtility.singleLineHeight;
            position.x += position.width - ButtonWidth;

            Rect buttonRect = new Rect(position.position, new Vector2(ButtonWidth, EditorGUIUtility.singleLineHeight));
            if (GUI.Button(buttonRect, new GUIContent("Find", "Opens an object picker dialog, which injects in this field the asset's path."), EditorStyles.miniButtonRight))
            {
                OpenFilePanel();
            }

            ProcessEventCommand(property);
        }

        private void ProcessEventCommand(SerializedProperty property)
        {
            switch (Event.current.commandName)
            {
                case "ObjectSelectorUpdated":
                    if (EditorGUIUtility.GetObjectPickerControlID() == _currentPickerWindow)
                    {
                        var prefab = EditorGUIUtility.GetObjectPickerObject();
                        string path = GetAssetPath(prefab);

                        property.stringValue = path;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    break;
                case "ObjectSelectorClosed":
                    _currentPickerWindow = -1;
                    break;
            }
        }

        private string GetAssetPath(UnityEngine.Object prefab)
        {
            string path = !prefab ? string.Empty : AssetDatabase.GetAssetPath(prefab);
            path = path.Substring(0, path.IndexOf('.'));
            path = path.Remove(0, PathPrefix.Length);
            return path;
        }

        private void OpenFilePanel()
        {
            _currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, null, _currentPickerWindow);
        }
    }
#endif
}
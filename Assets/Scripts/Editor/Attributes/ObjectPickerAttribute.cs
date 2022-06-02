using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Editor
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class ObjectPickerAttribute : PropertyAttribute
    {
        public ObjectPickerAttribute()
        {

        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ObjectPickerAttribute))]
    class ObjectPickerAttributeDrawer : PropertyDrawer
    {
        private int _currentPickerWindow;
        private UnityEngine.Object _prefab;
        private const string PathPrefix = "Assets/Resources/";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2.5f;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String) return;

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));

            position.y += EditorGUIUtility.singleLineHeight;
            position.x += position.width - 50;

            Rect buttonRect = new Rect(position.position, new Vector2(50, EditorGUIUtility.singleLineHeight));
            if (GUI.Button(buttonRect, "Find", EditorStyles.miniButtonRight))
            {
                OpenFilePanel();
            }

            switch (Event.current.commandName)
            {
                case "ObjectSelectorUpdated":
                    if (EditorGUIUtility.GetObjectPickerControlID() == _currentPickerWindow)
                    {
                        _prefab = EditorGUIUtility.GetObjectPickerObject();
                        string path = !_prefab ? string.Empty : AssetDatabase.GetAssetPath(_prefab);

                        path = path.Substring(0, path.IndexOf('.'));
                        path = path.Remove(0, PathPrefix.Length);

                        property.stringValue = path;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    break;
                case "ObjectSelectorClosed":
                    _currentPickerWindow = -1;
                    break;
            }
        }

        private void OpenFilePanel()
        {
            _currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, null, _currentPickerWindow);
        }
    }
#endif
}
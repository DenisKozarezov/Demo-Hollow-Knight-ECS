using System;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class ObjectPickerAttribute : PropertyAttribute
    {
        public ObjectPickerAttribute()
        {

        }
    }

    [CustomPropertyDrawer(typeof(ObjectPickerAttribute))]
    class ObjectPickerAttributeDrawer : PropertyDrawer
    {
        private int _currentPickerWindow;
        private UnityEngine.Object _prefab;
        private const string PathPrefix = "Assets/Resources/";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(property.displayName));
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Find", EditorStyles.miniButtonRight, GUILayout.Width(50)))
            {
                OpenFilePanel();
            }
            EditorGUILayout.EndHorizontal();

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
}
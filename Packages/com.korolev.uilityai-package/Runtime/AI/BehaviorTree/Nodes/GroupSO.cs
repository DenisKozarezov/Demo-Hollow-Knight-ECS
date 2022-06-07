using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public class GroupSO : ScriptableObject
    {
        [HideInInspector] public List<Node> Nodes = new List<Node>();
        public string Title;

        /************ ПОЛЯ ДЛЯ ХРАНЕНИЯ ДАННЫХ ОТОБРАЖЕНИЯ ***************************/
#if UNITY_EDITOR
        private Vector2 _position;
        [HideInInspector] public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                EditorUtility.SetDirty(this);
            }
        }
        [HideInInspector] public string GUID;
    #endif
    }
}
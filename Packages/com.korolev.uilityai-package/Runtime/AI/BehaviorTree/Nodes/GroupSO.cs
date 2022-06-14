using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public class GroupSO : ScriptableObject, IEquatable<GroupSO>
    {
        [HideInInspector] public List<Node> Nodes = new List<Node>();
        public string Title;

        public GroupSO Clone()
        {
            GroupSO clone = Instantiate(this);
            clone.Nodes = Nodes.ConvertAll(child => child.Clone());
            clone.Title = clone.Title.Clone().ToString();
            return clone;
        }
        public bool Equals(GroupSO other)
        {
            return other.GUID.Equals(GUID);
        }

        /************ ПОЛЯ ДЛЯ ХРАНЕНИЯ ДАННЫХ ОТОБРАЖЕНИЯ ***************************/
#if UNITY_EDITOR
        [SerializeField, HideInInspector]
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
#endif
        [HideInInspector] public string GUID;
    }
}
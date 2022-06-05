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
        [HideInInspector] public Vector2 Position;
        [HideInInspector] public string  GUID;
        
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public Vector2 GetPosition() { return Position; }
    #endif
    }
}
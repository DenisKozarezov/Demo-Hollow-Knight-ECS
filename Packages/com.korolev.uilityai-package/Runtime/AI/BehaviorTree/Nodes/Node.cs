/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI.BehaviorTree.Nodes
{
    public abstract class Node : ScriptableObject, IEquatable<Node>
    {
        protected EcsWorld _world = null;        
        public IManipulator UnGroupManipulator = null;
        
        [NonSerialized] public State  State = State.Running;
        [NonSerialized] public bool   Started = false;

        protected BehaviorTree BehaviorTreeRef;
        [HideInInspector] public Node Parent;
        [HideInInspector] public GroupSO GroupSo = null;
        
        public void Init(BehaviorTree tree, EcsWorld ecsWorld)
        {
            BehaviorTreeRef = tree;
            _world = ecsWorld;
            OnInit();
        }
        public State Update() 
        {
            if (!Started) 
            {
                OnStart();
                Started = true;
            }
            State = OnUpdate();
            if (State == State.Failure || State == State.Success) 
            {
                OnStop();
                Started = false;
            }
            return State;
        }
        protected abstract void OnInit();
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
        
        public virtual float Cost(ParameterNode parameter) { return 1; }
        public virtual float Cost() { return 1; }

        public abstract IEnumerable<Node> GetChildren();
        public abstract Node Clone();
        public bool Equals(Node other)
        {
            return other.GUID.Equals(GUID);
        }

        /************ ПОЛЯ ДЛЯ ХРАНЕНИЯ ДАННЫХ ОТОБРАЖЕНИЯ ***************************/
#if UNITY_EDITOR
        [SerializeField] [HideInInspector]
        private Vector2 _position;

        public Vector2 Position
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
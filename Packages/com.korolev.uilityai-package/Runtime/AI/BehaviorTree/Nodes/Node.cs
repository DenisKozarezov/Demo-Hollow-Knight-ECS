/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI.BehaviorTree.Nodes
{
    public abstract class Node : ScriptableObject
    {
        // Переменная _world автоматически инициализируется
        protected EcsWorld _world = null;        
        public IManipulator UnGroupManipulator = null;
        
        [NonSerialized] public State  State = State.Running;
        [NonSerialized] public bool   Started = false;
        
        [HideInInspector] public BehaviorTree BehaviorTreeRef;
        [HideInInspector] public Node Parent;
        [HideInInspector] public GroupSO GroupSo = null;
        

        public State Update() {
            if (!Started) {
                OnStart();
                Started = true;
            }
            State = OnUpdate();
            if (State == State.Failure || State == State.Success) {
                OnStop();
                Started = false;
            }
            return State;
        }
        public virtual void OnInit(){}
        public void OnInit(EcsWorld ecsWorld) { _world = ecsWorld; }
        public virtual void OnStart() { State = State.Running; }
        public virtual void OnStop() { }
        public abstract State OnUpdate();
        
        public virtual float Cost(ParameterNode parametr) { return 1; }
        public virtual float Cost() { return 1; }
        
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
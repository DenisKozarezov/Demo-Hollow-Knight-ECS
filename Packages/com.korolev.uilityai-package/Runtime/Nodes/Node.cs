using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace BehaviourTree.Runtime.Nodes
{
    public enum State : byte
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node : ScriptableObject, IEquatable<Node>
    {
        [HideInInspector] public State State = State.Running;
        [HideInInspector] public bool Started = false;
        [SerializeField, HideInInspector] public string GUID;

        protected IEntity Agent;

        public void Init(IEntity agent)
        {
            Agent = agent;
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
        public abstract IEnumerable<Node> GetChildren();
        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        public bool Equals(Node other)
        {
            if (other == null) return false;
            return GUID.Equals(other.GUID);
        }

#if UNITY_EDITOR
        [SerializeField, HideInInspector] public string Name;
        [SerializeField, HideInInspector] public Vector2 Position;    
        public abstract void AddChild(Node node);
        public abstract void RemoveChild(Node node);
#endif
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Conversation/Create Context")]
    public class ConversationContext : ScriptableObject, IEquatable<ConversationContext>
    {
        [SerializeField]
        private uint _id;
        [SerializeField]
        private bool _isLoop;
        [SerializeField, TextArea]
        private string[] _conversation;

        public uint ID => _id;
        public bool IsLoop => _isLoop;
        public Queue<string> Conversation => new Queue<string>(_conversation);

        public bool Equals(ConversationContext other)
        {
            return other._id == _id;
        }
    }
}
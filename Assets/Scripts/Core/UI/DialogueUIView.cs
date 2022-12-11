using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Core.Models;

namespace Core.UI
{
    public class DialogueUIView : UIBaseView
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private TextMeshProUGUI _text;

        private ConversationContext _context;
        private Coroutine _coroutine;
        private int _currentIndex;
        private int _phrasesCount;
        public bool IsConversating => _currentIndex < _phrasesCount;

        public event Action ConversationEnded;

        private void Start() => SetActive(false);
        private IEnumerator TextTypingCoroutine(string message, float typingSpeed = 1f)
        {
            _text.text = string.Empty;
            int length = message.Length;
            while (message.Length > 0)
            {
                _text.text += message.Substring(0, 1);
                message = message.Substring(1, message.Length - 1);
                yield return new WaitForSeconds(typingSpeed / length);
            }
        }
        public void SetConversationContext(ConversationContext context)
        {
            _context = context;
            _currentIndex = 0;
            _phrasesCount = context.Conversation.Count;
        }
        public void PlayNext()
        {
            if (_context is null || !IsConversating)
            {
                CloseDialog();
                ConversationEnded?.Invoke();
                return;
            }

            _text.text = _context.Conversation[_currentIndex];
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(TextTypingCoroutine(_context.Conversation[_currentIndex]));
            _currentIndex++;
        }
        public void OpenDialog() => Fade(_canvasGroup, FadeMode.On);
        public void CloseDialog() => Fade(_canvasGroup, FadeMode.Off);
    }
}
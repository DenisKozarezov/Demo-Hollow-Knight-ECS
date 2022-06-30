using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Core.Models;

namespace Core.UI
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private const float FadeTime = 2f;
        private const float TypingSpeed = 1f;
        private ConversationContext _context;
        private int _currentIndex;
        private int _phrasesCount;
        public bool IsConversating => _currentIndex < _phrasesCount;

        public event Action ConversationEnded;

        private void Start()
        {
            ResetColor();
            gameObject.SetActive(false);
        }
        public void SetConversationContext(ConversationContext context)
        {
            _context = context;
            _currentIndex = 0;
            _phrasesCount = context.Conversation.Count;
        }
        public void PlayNext()
        {
            if (_context == null || !IsConversating)
            {
                CloseDialog();
                ConversationEnded?.Invoke();
                return;
            }

            _text.text = _context.Conversation[_currentIndex];
            StartCoroutine(SequentialCoroutine(_context.Conversation[_currentIndex]));
            _currentIndex++;
        }
        public void OpenDialog()
        {
            gameObject.SetActive(true);
            ResetColor();
            var sequence = DOTween.Sequence();
            foreach (var image in gameObject.GetComponentsInChildren<MaskableGraphic>())
            {
                sequence.Join(image.DOColor(image.color.SetAlpha(1f), FadeTime));
            }
        }
        public void CloseDialog()
        {
            var sequence = DOTween.Sequence();
            foreach (var image in gameObject.GetComponentsInChildren<MaskableGraphic>())
            {
                sequence.Join(image.DOColor(image.color.SetAlpha(0f), FadeTime));
            }
            sequence.OnComplete(() => gameObject.SetActive(false));
        }
        private void ResetColor()
        {
            foreach (var image in gameObject.GetComponentsInChildren<MaskableGraphic>())
            {
                image.color = image.color.SetAlpha(0f);
            }
        }

        private IEnumerator SequentialCoroutine(string message)
        {
            _text.text = string.Empty;
            int length = message.Length;
            while (message.Length > 0)
            {
                _text.text += message.Substring(0, 1);
                message = message.Substring(1, message.Length - 1);
                yield return new WaitForSeconds(TypingSpeed / length);
            }
        }
    }
}
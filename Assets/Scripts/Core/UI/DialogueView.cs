using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Core.Models;
using UnityEngine.UI;

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
        private bool PhrasesEnded => _currentIndex >= _phrasesCount;

        public event Action ConversationEnded;

        private void Start()
        {
            OpenDialog();
            StartCoroutine(SequentialCoroutine("Ho there, traveller! I'm afraid there is only me to offer welcome. Our town's fallen quiet you see."));
        }
        public void SetConversationContext(ConversationContext context)
        {
            _context = context;
            _phrasesCount = context.Conversation.Count;
        }
        public void PlayNext()
        {
            if (PhrasesEnded)
            {
                CloseDialog();
                return;
            }

            _text.text = _context.Conversation.Peek();
            StartCoroutine(SequentialCoroutine(_context.Conversation.Peek()));
            _currentIndex++;
        }
        public void OpenDialog()
        {
            gameObject.SetActive(true);
            var objects = gameObject.GetComponentsInChildren<MaskableGraphic>();
            foreach (var image in objects)
            {
                image.color = image.color.SetAlpha(0f);
            }
            var sequence = DOTween.Sequence();
            foreach (var image in objects)
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
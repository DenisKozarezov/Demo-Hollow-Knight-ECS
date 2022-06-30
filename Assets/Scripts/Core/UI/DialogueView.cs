using System;
using System.Collections;
using System.Text;
using UnityEngine;
using TMPro;
using Core.Models;

namespace Core.UI
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private const float TypingSpeed = 1f;
        private ConversationContext _context;
        private int _currentIndex;
        private int _phrasesCount;
        private bool PhrasesEnded => _currentIndex >= _phrasesCount;

        public event Action ConversationEnded;

        public void SetDialogueContext(ConversationContext context)
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

            StartCoroutine(SequentialCoroutine(_context.Conversation.Peek(), TypingSpeed));
            _currentIndex++;
        }
        public void OpenDialog()
        {
            gameObject.SetActive(true);
        }
        public void CloseDialog()
        {
            gameObject.SetActive(false);
        }
        private IEnumerator SequentialCoroutine(string message, float time)
        {
            _text.text = "";
            int index = 0;
            StringBuilder builder = new StringBuilder(message);
            builder.Clear();
            float elapsedTime = 0f;
            while (index < message.Length)
            {                
                builder.Append(message[index]);

                // Space-symbol
                if (index + 1 < message.Length && message[index + 1] == ' ')
                {
                    builder.Append(' ');
                    index++;
                }
                _text.text = builder.ToString();
                index++;
                float _time = time / message.Length;
                elapsedTime += _time;
                yield return new WaitForSecondsRealtime(time / message.Length);
                Debug.Log(elapsedTime);
            }
        }
    }
}
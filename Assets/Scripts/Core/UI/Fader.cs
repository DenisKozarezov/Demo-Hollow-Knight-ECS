using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Core.UI
{
    [RequireComponent(typeof(RawImage))]
    public class Fader : MonoBehaviour
    {
        private RawImage _vignette;

        private void Awake()
        {
            _vignette = GetComponent<RawImage>();
        }
        public void Fade(FadeMode mode, float time)
        {
            gameObject.SetActive(true);
            float alpha = mode == FadeMode.On ? 1f : 0f;

            if (time == 0f)
            {
                _vignette.color = _vignette.color.WithAlpha(alpha);
                return;
            }

            _vignette.DOFade(alpha, time)
                .SetLink(gameObject)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (mode == FadeMode.Off) gameObject.SetActive(false);
                });
        }
    }
}

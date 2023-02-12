using DG.Tweening;
using UnityEngine;

namespace Core.ECS.Behaviours
{
    public abstract class UIBaseView : EntityBehaviour
    {
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        protected Tweener Fade(CanvasGroup cg, FadeMode mode, float time = 2f)
        {
            if (mode == FadeMode.On) SetActive(true);

            float alpha = mode == FadeMode.On ? 1f : 0f;
            return DOTween.To(() => 1f - alpha, x => cg.alpha = x, alpha, time)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    if (mode == FadeMode.Off) SetActive(false);
                });
        }
    }
}

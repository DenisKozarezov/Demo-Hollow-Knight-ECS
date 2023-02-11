using UnityEngine;
using Core.UI;
using Core.ECS.Events;

namespace Core.ECS.Systems.UI
{
    public sealed class InteractablePromptUISystem/* : IEcsRunSystem*/
    {
        //private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        //private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        //private InteractablePrompt _prompt;
        
        //private const string PromptPath = "Prefabs/UI/Interactable Prompt";
  
        //void IEcsRunSystem.Run()
        //{
        //    foreach (var i in _enter)
        //    {
        //        ref var entity = ref _enter.Get1(i);
        //        Vector2 position = entity.Position + Vector2.up * entity.InteractableComponent.OffsetY;
        //        ref string label = ref entity.InteractableComponent.Label;
        //        ShowLabel(ref position, ref label);
        //    }
        //    foreach (var i in _exit) HideLabel();
        //}
        //private void HideLabel() => _prompt.Fade(FadeMode.Off, 0.5f);
        //private void ShowLabel(ref Vector2 position, ref string label)
        //{
        //    if (_prompt == null || !_prompt.IsPlaying)
        //    {
        //        var asset = Resources.Load<InteractablePrompt>(PromptPath);
        //        _prompt = GameObject.Instantiate(asset, position, Quaternion.identity);
        //        _prompt.SetText(label);

        //    }
        //    _prompt.Fade(FadeMode.On, 0.5f);
        //}
    }
}

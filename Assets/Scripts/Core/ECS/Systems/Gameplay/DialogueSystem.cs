using Leopotam.Ecs;
using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using System.Linq;

namespace Core.ECS.Systems
{
    public class DialogueSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTalkingWithNPCEvent> _filter = null;
        private readonly EcsFilter<DialogueViewComponent> _dialogue = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                foreach (var dialogue in _dialogue)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var npc = ref _filter.Get1(i).NPC;
                    ref var view = ref _dialogue.Get1(dialogue).View;

                    if (!view.IsConversating)
                    {
                        var conversation = npc.Conversations.FirstOrDefault();
                        if (conversation != null)
                        {
                            view.SetConversationContext(conversation);
                            view.OpenDialog();
                            npc.Conversations.RemoveAt(0);
                        }
                    }
                    view.PlayNext();
                    entity.Destroy();
                }
            }
        }
    }
}

using System.Linq;
using Leopotam.Ecs;
using Core.ECS.Events.Player;
using Core.UI;
using Core.Models;

namespace Core.ECS.Systems
{
    public sealed class DialogueSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTalkingWithNPCEvent> _filter = null;
        private readonly DialogueUIView _view;

        public DialogueSystem(DialogueUIView view)
        {
            _view = view;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                if (!_view.IsConversating)
                {
                    ref var npc = ref _filter.Get1(i).NPC;
                    ConversationContext conversation = npc.Conversations.FirstOrDefault();
                    if (conversation != null)
                    {
                        _view.SetConversationContext(conversation);
                        _view.OpenDialog();
                        _view.ConversationEnded += OnConversationEnded;
                        npc.Conversations.RemoveAt(0);
                    }
                }
                _view.PlayNext();
            }
        }
        private void OnConversationEnded()
        {
            _view.ConversationEnded -= OnConversationEnded;
            _view.CloseDialog();
        }
    }
}

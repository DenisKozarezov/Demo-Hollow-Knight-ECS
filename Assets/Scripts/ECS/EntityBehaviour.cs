using UnityEngine;
using Core.ECS.ViewListeners;

namespace Core.ECS
{
    public class EntityBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();

            var viewController = gameObject.AddComponent<UnityViewController>();
            viewController.InitializeView(Contexts.sharedInstance.game, entity);

            foreach (var listener in GetComponents<IEventListener>())
            {
                listener.RegisterListeners(entity);
            }
        }
    }
}

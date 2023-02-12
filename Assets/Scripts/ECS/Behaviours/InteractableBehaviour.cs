using UnityEngine;

namespace Core.ECS.Behaviours
{
    public sealed class InteractableBehaviour : CollisionEntityBehaviour
    {
        [SerializeField]
        private string _label;
        [SerializeField]
        private InteractType _interactType;

        private void Start() => Entity.AddInteractable(_label, _interactType);
    }
}
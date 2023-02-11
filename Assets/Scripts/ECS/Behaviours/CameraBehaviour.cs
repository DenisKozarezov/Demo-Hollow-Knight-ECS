using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.ECS.Behaviours
{
    public sealed class CameraBehaviour : EntityBehaviour
    {
        [SerializeReference]
        private Volume _volume;

        protected override void Awake()
        {
            base.Awake();
            Entity.AddVignette(_volume.profile.GetPostProcessSetting<Vignette>());
        }
    }
}

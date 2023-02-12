using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.ECS.Behaviours
{
    public sealed class CameraBehaviour : EntityBehaviour
    {
        [SerializeReference]
        private Volume _volume;

        protected override void Start()
        {
            Entity.AddVignette(_volume.profile.GetPostProcessSetting<Vignette>());
        }
    }
}

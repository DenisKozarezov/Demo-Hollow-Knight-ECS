using UnityEngine;
using UnityEngine.Rendering.Universal;
using Entitas;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraLowHealthVignetteSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<GameEntity> _vignettes;
        
        private const float IntensityMax = 0.55f;

        private float _currentVelocity;

        public CameraLowHealthVignetteSystem(GameContext game)
        {
            _players = game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentHp, GameMatcher.MaxHp));
            _vignettes = game.GetGroup(GameMatcher.Vignette);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _vignettes)
            {
                foreach (GameEntity player in _players)
                {
                    int currentHealth = player.currentHp.Value;
                    int maxHp = player.maxHp.Value;

                    if (currentHealth == 0 || currentHealth == maxHp) continue;

                    Vignette vignette = entity.vignette.Value;

                    float factor = Mathf.Clamp(1f - (float)currentHealth / maxHp, 0f, IntensityMax);
                    vignette.intensity.value = Mathf.SmoothDamp(vignette.intensity.value, factor, ref _currentVelocity, 0.3f);
                }
            }
        }
    }
}

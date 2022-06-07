/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Systems;
using Core.Models;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.Systems;
using Examples.Example_1.ECS.Systems.FalseKnight;
using Examples.Example_1.ECS.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Examples.Example_1.ECS
{
    public class SceneECSManager : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private PlayerInputController _playerInputController;

        [SerializeField] 
        private GameObject _prefabDustAnimation;
        [SerializeField]
        private UnitFactory _unitFactory;

        private void Awake()
        {
            _playerInputController = new PlayerInputController();
            _playerInputController.Enable();
        }
        private void Start ()
        {
            Time.timeScale = 1.4f;
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world)
                .ConvertScene() // Этот метод сконвертирует GO в Entity;
                .Add(new BehaviorTreeSystem())
                .Add(new PlayerMoveSystem(_playerInputController))
                .Add(new PlayerAnimationSystem(_playerInputController))
                .Add(new FalseKnightJumpAnimationSystem())
                .Add(new FalseKnightAttackAnimationSystem())
                .Add(new PlayerJumpSystem(_playerInputController))
                .Add(new DustCloudAnimationSystem(_prefabDustAnimation))
                .Add(new PlayerAttackSystem(_playerInputController))
                .Add(new DamageAnimationSystem())
                .Add(new DamageSystem())
                .Add(new HealthSystem())
                .Add(new GroundSystem())
                .Add(new UnitSpawnSystem(_unitFactory))
                .Add(new EnemyDeathEffectSystem())
                .Add(new CameraShakeAnimationSystem(Camera.main));        
                
            _systems?.Init();
        }

        private void FixedUpdate() 
        {
            _systems?.Run();
        }

        private void OnDestroy() 
        {
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}
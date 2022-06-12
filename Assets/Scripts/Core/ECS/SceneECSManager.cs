/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Systems;
using Core.Models;
using Core.Units;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.Systems;
using Examples.Example_1.ECS.Systems.FalseKnight;
using Examples.Example_1.ECS.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Examples.Example_1.ECS
{
    public class SceneECSManager : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private PlayerInputController _playerInputController;

        [Inject]
        private UnitScript _player;
        [Inject]
        private PlayerModel _playerModel;

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
            
                // General systems
                .Add(new UnitInitSystem())
                .Add(new DamageSystem())
                .Add(new HealthSystem())
                .Add(new GroundSystem())
                
                // Units systems               
                .Add(new UnitSpawnSystem(_unitFactory))
                .Add(new BehaviorTreeSystem())
                
                // Player systems
                .Add(new PlayerMoveSystem(_playerInputController))    
                .Add(new PlayerJumpSystem(_playerInputController, _playerModel))
                .Add(new PlayerAttackSystem(_playerInputController, _playerModel))
                .Add(new PlayerAttackCooldownSystem(_playerInputController, _playerModel))            
                .Add(new PlayerAnimationSystem(_playerInputController))

                // Other systems
                .Add(new FalseKnightJumpAnimationSystem())
                .Add(new FalseKnightAttackAnimationSystem())                
                .Add(new DustCloudAnimationSystem(_prefabDustAnimation))           
                .Add(new DamageAnimationSystem())        
                .Add(new EnemyDeathEffectSystem())
                .Add(new CameraShakeAnimationSystem(Camera.main));

            AddOneFrames();
            AddInjections();

            _systems?.Init();
        }

        private void FixedUpdate() 
        {
            _systems?.Run();
        }

        private void OnDestroy() 
        {
            _systems.Destroy();
            _world.Destroy();
        }

        private void AddInjections()
        {
            _systems.Inject(_player);
        }
        private void AddOneFrames()
        {
            _systems
                .OneFrame<UnitInitComponent>()
                .OneFrame<DamageEventComponent>()
                .OneFrame<UnitCreateEventComponent>();
        }
    }
}
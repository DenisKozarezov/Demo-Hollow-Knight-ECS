/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using AI.ECS.Systems;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.Systems;
using Examples.Example_1.ECS.Systems.FalseKnight;
using Examples.Example_1.ECS.Systems.Player;

namespace Examples.Example_1.ECS
{
    public class SceneECSManager : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
          
        [Inject]
        private readonly UnitScript _player = null;
        [Inject]
        private readonly IInputSystem _inputSystem = null;
        [Inject]
        private readonly UnitsDefinitions _unitsDefinitions = null;

        [SerializeField] 
        private GameObject _prefabDustAnimation;

        private void Start ()
        {            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world)
                .ConvertScene() // Этот метод сконвертирует GO в Entity;
            
                // Init systems
                .Add(new FalseKnightInitSystem(_unitsDefinitions.FalseKnight))
                .Add(new PlayerInitSystem(_unitsDefinitions.PlayerModel))
                .Add(new UnitInitSystem())
                
                // General systems
                .Add(new DamageSystem())
                .Add(new HealthSystem())
                .Add(new GroundSystem())
                
                // Units systems               
                .Add(new BehaviorTreeSystem())
                .Add(new DestroyEntitiesSystem())

                // Player systems       
                .Add(new PlayerMoveSystem(_inputSystem))    
                .Add(new PlayerJumpSystem(_inputSystem))
                .Add(new PlayerAttackSystem(_inputSystem, _unitsDefinitions.PlayerModel))
                .Add(new PlayerAttackCooldownSystem(_inputSystem, _unitsDefinitions.PlayerModel))            
                .Add(new PlayerAnimationSystem(_inputSystem))

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
                .OneFrame<UnitCreateEventComponent>()
                .OneFrame<DiedComponent>();
        }
    }
}
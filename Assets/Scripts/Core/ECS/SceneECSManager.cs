/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.Models;
using Core.ECS.Events;
using Core.ECS.Events.Player;
using Core.ECS.Systems;
using Core.ECS.Systems.FalseKnight;
using Core.ECS.Systems.Player;
using Core.ECS.Systems.UI;
using Core.ECS.Components.Units;
using Voody.UniLeo;
using Zenject;
using AI.ECS.Systems;

namespace Core.ECS
{
    public class SceneECSManager : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
          
        [Inject]
        private readonly IInputSystem _inputSystem = null;
        [Inject]
        private readonly UnitsDefinitions _unitsDefinitions = null;

        private void Start ()
        {            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world).ConvertScene(); // Этот метод сконвертирует GO в Entity;

            AddOtherSystems();
            AddInitSystems();
            AddGeneralSystems();
            AddPlayerSystems();
            AddUISystems();
            AddCameraSystems();
            AddCleanupSystems();

            AddOneFrames();

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

        private void AddOneFrames()
        {
            _systems
                .OneFrame<UnitInitComponent>()
                .OneFrame<DamageEventComponent>()
                .OneFrame<HitEventComponent>()
                .OneFrame<UnitCreateEventComponent>()
                .OneFrame<DiedComponent>()
                .OneFrame<PlayerRecievedDamageEvent>()
                .OneFrame<PlayerDiedEvent>()
                .OneFrame<PlayerHealedEvent>();
        }
        private void AddInitSystems()
        {
            _systems
                .Add(new FalseKnightInitSystem(_unitsDefinitions.FalseKnight))
                .Add(new PlayerInitSystem(_unitsDefinitions.PlayerModel))
                .Add(new UnitInitSystem());
        }
        private void AddGeneralSystems()
        {
            _systems
                .Add(new HitSystem())
                .Add(new DamageSystem())
                .Add(new HealthSystem())
                .Add(new GroundSystem())
                .Add(new BehaviorTreeSystem());
        }
        private void AddPlayerSystems()
        {
            _systems
                .Add(new PlayerRecievedDamageSystem())
                .Add(new PlayerMoveSystem(_inputSystem))
                .Add(new PlayerJumpSystem(_inputSystem))
                .Add(new PlayerAttackSystem(_inputSystem))
                .Add(new PlayerAttackCooldownSystem(_inputSystem))
                .Add(new PlayerAnimationSystem(_inputSystem));
        }
        private void AddCameraSystems()
        {
            _systems
                .Add(new CameraShakeSystem(Camera.main))
                .Add(new CameraFadeSystem(Camera.main));
                //.Add(new CameraFollowSystem(Camera.main));
        }
        private void AddOtherSystems()
        {
            _systems
                .Add(new FalseKnightJumpAnimationSystem())
                .Add(new FalseKnightAttackAnimationSystem())
                .Add(new DustCloudAnimationSystem())
                .Add(new DamageAnimationSystem())
                .Add(new EnemyDeathEffectSystem());
        }
        private void AddUISystems()
        {
            _systems
                .Add(new HealthViewInitSystem())
                .Add(new HealthViewReducedSystem())
                .Add(new HealthViewHealedSystem());
        }
        private void AddCleanupSystems()
        {
            _systems.Add(new DestroyEntitiesSystem());
        }
    }
}
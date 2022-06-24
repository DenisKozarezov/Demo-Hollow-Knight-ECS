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
using Core.ECS.Components.Units;
using Voody.UniLeo;
using Zenject;

namespace Core.ECS
{
    public class SceneECSManager : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private AllSystems _allSystems;
          
        [Inject]
        private readonly IInputSystem _inputSystem = null;
        [Inject]
        private readonly UnitsDefinitions _unitsDefinitions = null;

        private void Start ()
        {            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world).ConvertScene(); // Этот метод сконвертирует GO в Entity;
            
            GameContext context = new GameContext(_systems, _inputSystem, _unitsDefinitions);
            _allSystems = new AllSystems(context);
            
            AddOneFrames();

            _allSystems.Init();
        }
        private void FixedUpdate() 
        {
            _allSystems.Run();
        }
        private void OnDestroy() 
        {
            _allSystems.Destroy();
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
                .OneFrame<PlayerHealedEvent>()
                .OneFrame<EnergyReducedEvent>();
        }
    }
}
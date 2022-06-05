/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.ECS.Systems;
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
        EcsWorld _world;
        EcsSystems _systems;
        private PlayerInputController _playerInputController;

        [SerializeField] private GameObject PrefubDustAnimation;
        
        void Start ()
        {
            Time.timeScale = 1.4f;
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FalseKnight"), LayerMask.NameToLayer("Character"));
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FalseKnight"), LayerMask.NameToLayer("FireBall"));
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("FireBall"));
            
            _playerInputController = new PlayerInputController();
            _world = new EcsWorld ();
            _systems = new EcsSystems(_world)
                .ConvertScene() // Этот метод сконвертирует GO в Entity;
                .Add(new BehaviorTreeSystem())
                .Add(new PlayerMoveSystem(_playerInputController))
                .Add(new PlayerAnimationSystem(_playerInputController))
                .Add(new FalseKnightJumpAnimationSystem())
                .Add(new FalseKnightAttackAnimationSystem())
                .Add(new PlayerJumpSystem(_playerInputController))
                .Add(new DustCloudAnimationSystem(PrefubDustAnimation))
                .Add(new PlayerAttackSystem(_playerInputController))
                .Add(new DamageAnimationSystem())
                .Add(new CameraShakeAnimationSystem(Camera.main));
                
            _systems.Init ();
        }
    
        void FixedUpdate () {
            _systems.Run ();
        }

        void OnDestroy () {
            _systems.Destroy ();
            _world.Destroy ();
        }
    }
}
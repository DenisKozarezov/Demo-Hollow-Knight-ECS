using UnityEngine;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems
{
    public class DustCloudAnimationSystem : MonoBehaviour, IEcsInitSystem, IEcsRunSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<AnimateDustEventComponent> _filter = null;

        private float TimeAliveSeconds = 0.7f;

        private float timePlayDustAnimationOnScene = 2;

        private GameObject _prefubDustAnimation;

        public DustCloudAnimationSystem(GameObject prefubDustAnimation)
        {
            _prefubDustAnimation = prefubDustAnimation;
        }

        public void Init()
        {

        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity(i);
                if (ecsEntity.Get<AnimateDustEventComponent>().DustAnimation == null)
                {
                    GameObject dustAnimation = Instantiate(_prefubDustAnimation);//= (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Examples/Example 1/Prefubs/Dust.prefab", typeof(GameObject)));
                    dustAnimation.transform.position = ecsEntity.Get<AnimateDustEventComponent>().Parent.position;
                    dustAnimation.transform.rotation = Quaternion.identity;
                    dustAnimation.transform.localScale = ecsEntity.Get<AnimateDustEventComponent>().Scale;
                    dustAnimation.transform.parent = ecsEntity.Get<AnimateDustEventComponent>().Parent;
                    dustAnimation.transform.parent = null;

                    ecsEntity.Get<AnimateDustEventComponent>().DustAnimation = dustAnimation;
                }
                else
                {
                    ecsEntity.Get<AnimateDustEventComponent>().TimeAlive += Time.deltaTime;
                    if (ecsEntity.Get<AnimateDustEventComponent>().TimeAlive > TimeAliveSeconds)
                    {
                        Destroy(ecsEntity.Get<AnimateDustEventComponent>().DustAnimation);
                        ecsEntity.Del<AnimateDustEventComponent>();
                    }
                }
            }
        }
    }
}
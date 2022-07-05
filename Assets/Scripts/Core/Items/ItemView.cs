using UnityEngine;
using Core.ECS.Events.Player;
using Voody.UniLeo;

namespace Core
{
    public class ItemView : MonoBehaviour, IPoolObject<ItemView>
    {
        [SerializeField]
        private ItemType _itemType;

        public IPoolContainer<ItemView> Container { private set; get; }

        public void SetPool(IPoolContainer<ItemView> pool)
        {
            Container = pool;
        }
        void IPoolObject<ItemView>.Release()
        {
            gameObject.SetActive(false);
        }
        void IPoolObject<ItemView>.Reset()
        {
            gameObject.SetActive(true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                switch (_itemType)
                {
                    case ItemType.Geo:
                        WorldHandler.GetWorld().NewEntity(new PlayerObtainedGeoEvent { Value = 3 });
                        break;
                }
                Container.Release(this);
            }
        }
    }
}
using UnityEngine;
using Core.ECS.Events.Player;
using Voody.UniLeo;

namespace Core
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private ItemType _itemType;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                switch (_itemType)
                {
                    case ItemType.Geo:
                        WorldHandler.GetWorld().NewEntity(new PlayerObtainedGeoEvent { Value = 1 });
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
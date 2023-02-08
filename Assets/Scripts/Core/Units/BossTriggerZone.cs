using UnityEngine;
using UnityEngine.Events;
using Core.Models;
using Core.ECS.Events.Player;
using Voody.UniLeo;

namespace Core.Units
{
    //public class BossTriggerZone : MonoBehaviour
    //{
    //    [SerializeField]
    //    private EnemyModel _enemyModel;
    //    [SerializeField]
    //    private UnityEvent PlayerEnteredZone;

    //    private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //        if (collision.gameObject.layer == Constants.PlayerLayer)
    //        {
    //            WorldHandler.GetWorld().NewEntity(new PlayerEnteredBossZoneEvent
    //            {
    //                BossModel = _enemyModel
    //            });
    //            PlayerEnteredZone.Invoke();
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
using System.Collections.Generic;
using UnityEngine;
//using Core.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Unit Factory")]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField]
        private List<UnitModel> _units;

        public GameObject GetUnit<T>() where T : UnitModel
        {
            var config = _units.Find(x => x is T);
            var prefab = Resources.Load<GameObject>(config.PrefabPath);
            return GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
        public GameObject GetRandomUnit()
        {
            var config = _units[Random.Range(0, _units.Count)];
            var prefab = Resources.Load<GameObject>(config.PrefabPath);
            return GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
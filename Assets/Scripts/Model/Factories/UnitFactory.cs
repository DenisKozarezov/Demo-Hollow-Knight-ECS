using System.Collections.Generic;
using UnityEngine;
using Core.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Unit Factory")]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField]
        private List<UnitModel> _units;

        protected UnitModel GetConfig<T>() where T : UnitModel
        {
            return _units.Find(x => x is T);
        }
        protected UnitModel GetRandomConfig()
        {
            return _units[Random.Range(0, _units.Count)];
        }

        public UnitView GetUnit<T>() where T : UnitModel
        {
            UnitModel config = GetConfig<T>();
            return null;
            //return Services.UnitsManager.InstantiateUnit(config.ID);
        }
        public UnitView GetRandomUnit()
        {
            UnitModel config = GetRandomConfig();
            return null;
            //return Services.UnitsManager.InstantiateUnit(config.ID);
        }

        public void AddUnit(UnitModel model)
        {
            if (!_units.Contains(model)) _units.Add(model);
        }

        public UnitFactory Clone()
        {
            List<UnitModel> list = new List<UnitModel>();
            list.AddRange(_units);
            return new UnitFactory
            {
                _units = list
            };
        }
    }
}
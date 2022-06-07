using UnityEngine;

namespace Core.Models
{
    public interface IUnitFactory
    {
        public GameObject GetUnit<T>() where T : UnitModel;
        public GameObject GetRandomUnit();
    }
}
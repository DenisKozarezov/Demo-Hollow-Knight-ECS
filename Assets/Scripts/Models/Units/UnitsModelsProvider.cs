using System;
using System.Linq;
using UnityEngine;

namespace Core.Models
{
    public sealed class UnitsModelsProvider
    {
        private readonly Lazy<UnitModel[]> _models;
        public UnitsModelsProvider()
        {
            _models = new Lazy<UnitModel[]>(() => Resources.LoadAll<UnitModel>("Scriptable Objects/Units"));
        }
        public T Resolve<T>() where T : UnitModel
        {
            UnitModel result = _models.Value.FirstOrDefault(x => x is T);

            if (result is null) throw new NullReferenceException();

            return (T)result;
        }
    }
}

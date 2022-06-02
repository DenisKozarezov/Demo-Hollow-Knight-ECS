using Core.Units;

namespace Core.Models
{
    public interface IUnitFactory
    {
        public UnitView GetUnit<T>() where T : UnitModel;
        public UnitView GetRandomUnit();
    }
}
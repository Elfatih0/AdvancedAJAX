using AdvancedAJAX.Models;

namespace AdvancedAJAX.Interfaces
{
    public interface IUnit
    {
        List<Unit> GetItems();

        Unit GetUnit(int id);

        Unit Create(Unit unit);

        Unit Edit(Unit unit);

        Unit Delete(Unit unit);

    }
}

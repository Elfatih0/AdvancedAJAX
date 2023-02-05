using AdvancedAJAX.Models;

namespace AdvancedAJAX.Interfaces
{
    public interface ICountry
    {
        List<Country> GetItems();

        Country GetUnit(int id);

        Country Create(Country country);

        Country Edit(Country country);

        Country Delete(Country country);
    }
}

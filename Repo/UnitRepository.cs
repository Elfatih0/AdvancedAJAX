using AdvancedAJAX.Data;
using AdvancedAJAX.Interfaces;
using AdvancedAJAX.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAJAX.Repo
{
    public class UnitRepository : IUnit
    {
        private readonly AppDbContext _context;

        public UnitRepository(AppDbContext context)
        {
            _context = context;
        }

        public Unit Create(Unit unit)
        {

                _context.Units.Add(unit);
                _context.SaveChanges();

            return unit;
        }

        public Unit Delete(Unit unit)
        {
            _context.Units.Remove(unit);
            _context.SaveChanges();

            return unit;
        }

        public Unit Edit(Unit unit)
        {
            _context.Update(unit);
            _context.SaveChanges();

            return unit;
        }

        public List<Unit> GetItems()
        {
            List<Unit> units = _context.Units.ToList();
            return units;
        }



        public Unit GetUnit(int id)
        {

            var unit = _context.Units
                .FirstOrDefault(m => m.Id == id);

            return unit;
        }
    }
}

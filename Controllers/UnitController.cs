using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvancedAJAX.Data;
using AdvancedAJAX.Models;
using AdvancedAJAX.Interfaces;

namespace AdvancedAJAX.Controllers
{
    public class UnitController : Controller
    {


        // GET: Unit
        public IActionResult Index()
        {
            List<Unit> units = _unitRepo.GetItems();
            return View(units);
        }

        private readonly AppDbContext _context;

        private readonly IUnit _unitRepo;

        public UnitController(IUnit unitRepo)
        {
            _unitRepo = unitRepo;
        }

        // GET: Unit/Details/5
        public  IActionResult Details(int id)
        {

            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }

        // GET: Unit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Unit unit)
        {

            unit=_unitRepo.Create(unit);

            return RedirectToAction(nameof(Index));
        }

        // GET: Unit/Edit/5
        public IActionResult Edit(int id)
        {


            Unit unit = _unitRepo.GetUnit(id);

            return View(unit);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unit = _unitRepo.Edit(unit);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Unit/Delete/5
        public IActionResult Delete(int id)
        {


            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Unit unit)
        {
            if (_context.Units == null)
            {
                return Problem("Entity set 'AppDbContext.Units'  is null.");
            }
             unit = _unitRepo.Delete(unit);
            
            return RedirectToAction(nameof(Index));
        }
        
        private bool UnitExists(int id)
        {
          return _context.Units.Any(e => e.Id == id);
        }
    }
}

﻿using AdvancedAJAX.Data;
using AdvancedAJAX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAJAX.Controllers
{
    public class CityController : Controller
    {

        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<City> Cities;
            Cities = _context.Cities.ToList();
            return View(Cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            City City = new City();
            ViewBag.Countries = GetCountries();
            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(City City)
        {

            _context.Add(City);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            City City = _context.Cities
              .Where(c => c.Id == Id).FirstOrDefault();

            return View(City);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            City City = _context.Cities
              .Where(c => c.Id == Id).FirstOrDefault();

            ViewBag.Countries = GetCountries();

            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(City City)
        {
            _context.Attach(City);
            _context.Entry(City).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            City City = _context.Cities
              .Where(c => c.Id == Id).FirstOrDefault();

            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(City City)
        {
            _context.Attach(City);
            _context.Entry(City).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private List<SelectListItem> GetCountries()
        {
            var lstCountries = new List<SelectListItem>();

            List<Country> Countries = _context.Countries.ToList();

            lstCountries = Countries.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.Name
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Country----"
            };

            lstCountries.Insert(0, defItem);

            return lstCountries;
        }


        //***************************************************************************this is for the dialog ************************************************************
        [HttpGet]
        public IActionResult CreateModalForm(int countryId)
        {
            City city = new City();
            city.CountryId = countryId;
            city.CountryName = GetCountryName(countryId);
            return PartialView("_CreateModalForm",city);
        }


        [HttpPost]
        public IActionResult CreateModalForm(City city)
        {
            _context.Add(city);
            _context.SaveChanges();
            return NoContent();
        }


        //get name of the country from the db by passing the country id as a param
        private string GetCountryName(int countryId)
        {
            if (countryId == 0)
                return "";

            string strCountryName = _context.Countries
                .Where(ct => ct.Id == countryId)
                .Select(nm => nm.Name).Single().ToString();

            return strCountryName;

        }

        //***************************************************************************this is for the dialog end************************************************************


    }
}

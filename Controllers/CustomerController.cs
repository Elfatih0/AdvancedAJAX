﻿using AdvancedAJAX.Data;
using AdvancedAJAX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAJAX.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        //============================================= this is for photo upload START =================================================================== *@
        private readonly IWebHostEnvironment _webHost;
        //============================================= this is for photo upload END =================================================================== *@



        //============================================= INCLUDED FOR photo upload START =================================================================== *@

        public CustomerController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        //============================================= INCLUDED FOR photo upload END =================================================================== *@


        public IActionResult Index()
        {
            List<Customer> Cities;
            Cities = _context.Customers.ToList();
            return View(Cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Customer Customer = new Customer();
            ViewBag.Countries = GetCountries();
            return View(Customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
                    //============================================= this is for photo upload START =================================================================== *@

            string uniqueFileName = GetProfilePhotoFileName(customer);
            customer.PhotoUrl = uniqueFileName;
            //============================================= this is for photo upload END =================================================================== *@


            _context.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Customer customer = _context.Customers.Where(c => c.Id == Id).FirstOrDefault();
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Customer customer = _context.Customers.Where(c => c.Id == Id).FirstOrDefault();
            ViewBag.Countries = GetCountries();
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _context.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Customer customer = _context.Customers.Where(c => c.Id == Id).FirstOrDefault();
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _context.Attach(customer);
            _context.Entry(customer).State = EntityState.Deleted;
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

        [HttpGet]
        public JsonResult GetCitiesByCountry(int countryId)
        {

            List<SelectListItem> cities = _context.Cities
              .Where(c => c.CountryId == countryId)
              .OrderBy(n => n.Name)
              .Select(n =>
              new SelectListItem
              {
                  Value = n.Id.ToString(),
                  Text = n.Name
              }).ToList();

            return Json(cities);

        }


        //============================================= this is for photo upload START =================================================================== *@

        private string GetProfilePhotoFileName(Customer customer)
        {
            string uniqueFileName = null;

            if (customer.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    customer.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        //============================================= this is for photo upload END =================================================================== *@


    }
}
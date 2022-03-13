using System;
using System.Collections.Generic;
using CentiroHomeAssignment.Data;
using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CentiroHomeAssignment.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public OrdersController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        /// <summary>
        /// Create Order - get request
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create Order - post request
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order ord)
        {
            _db.Order.Add(ord);
            _db.SaveChanges();
            // Display all Orders
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Create Order - post request
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateFromFiles()
        {
            //Get Path to import files from appSettings.json
            ImporterOptions options = new ImporterOptions();
            options.Directory = _configuration["Import:DirectoryPath"];
            options.Pattern = _configuration["Import:Pattern"];

            ImporterService import = new ImporterService(options);
            var orders = import.LoadFiles();

            orders.ForEach(x => _db.Order.Add(x));
            _db.SaveChanges();
            // Display all Orders
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Get all data from db
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAll()
        {
            IEnumerable<Order> orders = _db.Order;
            return View(orders);
        }

        /// <summary>
        /// Get Orders with same number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public IActionResult GetByOrderNumber(string orderNumber)
        {
            // TODO: Return the specific order to a view

            throw new NotImplementedException();
        }
    }
}

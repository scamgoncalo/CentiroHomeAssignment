using System;
using System.Collections.Generic;
using CentiroHomeAssignment.Data;
using CentiroHomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentiroHomeAssignment.Controllers
{
    public class OrdersController : Controller
    {
        public readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
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
        public IActionResult Create(Order ord)
        {
            _db.Order.Add(ord);
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

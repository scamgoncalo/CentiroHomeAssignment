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

        public IActionResult GetAll()
        {
            IEnumerable<Order> orders = _db.Order;
            return View(orders);
        }

        public IActionResult GetByOrderNumber(string orderNumber)
        {
            // TODO: Return the specific order to a view

            throw new NotImplementedException();
        }
    }
}

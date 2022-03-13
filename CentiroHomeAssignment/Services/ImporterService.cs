using CentiroHomeAssignment.Data;
using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public class ImporterService
    {
        private readonly ImporterOptions _options;
        private readonly ApplicationDbContext _db;

        public ImporterService(ApplicationDbContext db, ImporterOptions options)
        {
            _options = options;
            _db = db;
        }

        public List<Order> LoadFiles(FileHandler fileHandler)
        {
            List<Order> newOrders = new List<Order>();

            // Load data from files without being attached to a specific kind of files
            var ordersInFiles = fileHandler.LoadFiles(_options.Directory, _options.Pattern);

            foreach (var order in ordersInFiles)
            {
                // Verify if Order exists in DB
                Order orderFromDb = OrderAlreadyExists(order);

                // if order is new
                if (orderFromDb == null || object.Equals(orderFromDb, default(Order)))
                {
                    newOrders.Add(order);
                }
            }
                return newOrders;
        }

        private Order OrderAlreadyExists(Order ord)
        {
            return _db.Order.Where(
                        o => (o.OrderNumber == ord.OrderNumber) &&
                           (o.OrderLineNumber == ord.OrderLineNumber)
                        ).FirstOrDefault();
        }
    }
}

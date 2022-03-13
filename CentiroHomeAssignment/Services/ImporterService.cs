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

        public List<Order> LoadFiles()
        {
            List<Order> orders = new List<Order>();
            try
            {
                // Only get files that follow the correct pattern
                string[] files = Directory.GetFiles(_options.Directory, _options.Pattern);
                foreach (string filePath in files)
                {
                    bool firstLine = true;
                    string line;
                    StreamReader file = new StreamReader(filePath);

                    while ((line = file.ReadLine()) != null)
                    {
                        //Skip first line
                        if (firstLine)
                        {
                            firstLine = false;
                            continue;
                        }

                        var data = line.Split('|');

                        Order ord = new Order();

                        ord.OrderNumber = int.Parse(data[1].Trim(), NumberFormatInfo.InvariantInfo);
                        ord.OrderLineNumber = int.Parse(data[2].Trim(), NumberFormatInfo.InvariantInfo);

                        // Verify if Order exists in DB
                        Order orderFromDb = OrderAlreadyExists(ord);

                        // if order is new
                        if (orderFromDb == null || object.Equals(orderFromDb, default(Order)))
                        {
                            //Following properties are only fetched if order is new
                            ord.ProductNumber = data[3].Trim();
                            ord.Quantity = int.Parse(data[4].Trim(), NumberFormatInfo.InvariantInfo);
                            ord.Name = data[5].Trim();
                            ord.Description = data[6];
                            ord.Price = double.Parse(data[7].Trim(), NumberFormatInfo.InvariantInfo);
                            ord.ProductGroup = data[8].Trim();
                            ord.OrderDate = DateTime.Parse(data[9].Trim(), NumberFormatInfo.InvariantInfo);
                            ord.CustomerName = data[10].Trim();
                            ord.CustomerNumber = int.Parse(data[11].Trim(), NumberFormatInfo.InvariantInfo);
                            orders.Add(ord);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return orders;
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

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
        public readonly ApplicationDbContext _db;
        private string _path;
        
        public ImporterService(ApplicationDbContext db, string path)
        {
            _db = db;
            _path = path;
        }

        public List<Order> LoadFile()
        {
            List<Order> orders = new List<Order>();
            try
            {
                bool firstLine = true;
                string line;
                StreamReader file = new StreamReader(_path);

                //TODO: First line is the name of the variables. Skip line
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return orders;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProductGroup { get; set; }
        public int OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int CustomerNumber { get; set; }

    }
}

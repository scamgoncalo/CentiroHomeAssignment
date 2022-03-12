using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Models
{
    public class Order
    {
        // Since there are no foreseeable future a relational table at the moment, no foreign key was added
        // [ForeignKey(name string)]

        [Key]
        public int Id { get; set; }

        [DisplayName("Order Number")]
        public int OrderNumber { get; set; }

        [DisplayName("Order Line Number")]
        public int OrderLineNumber { get; set; }

        [DisplayName("Product Number")]
        public string ProductNumber { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        [DisplayName("Product Group")]
        public string ProductGroup { get; set; }

        [DisplayName("Order Date")]
        public string OrderDate { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Customer Number")]
        public int CustomerNumber { get; set; }

    }
}

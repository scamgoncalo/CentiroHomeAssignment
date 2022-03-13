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
        [Required]
        public int OrderNumber { get; set; }

        [DisplayName("Order Line Number")]
        [Required]
        public int OrderLineNumber { get; set; }

        [DisplayName("Product Number")]
        [Required]
        public string ProductNumber { get; set; }

        [Required]
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [DisplayName("Product Group")]
        [Required]
        public string ProductGroup { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Customer Number")]
        [Required]
        public int CustomerNumber { get; set; }

    }
}

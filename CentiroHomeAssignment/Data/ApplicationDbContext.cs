using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Data
{
    public class ApplicationDbContext : DbContext
    {

        //Create Order table in Db
        public DbSet<Order> Order { get; set; }

        //Configure DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

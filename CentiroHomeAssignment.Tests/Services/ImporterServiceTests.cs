using CentiroHomeAssignment.Controllers;
using CentiroHomeAssignment.Data;
using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Tests.Services
{
    [TestClass]
    public class ImporterServiceTests
    {
        private readonly ImporterService _importerService;
        private ApplicationDbContext _dbContext;
        private ImporterOptions options;

        //[TestInitialize]
        public ImporterServiceTests()
        {
            //-> It is possible to configure a use in memory Database for Unit Tests -> another possible approach
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseInMemoryDatabase();
            //var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            //Create a mock of the db could also be done
            var dbContextOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionBuilder.UseSqlServer("Server=.;Database=Centiro;Trusted_Connection=True;MultipleActiveResultSets=True");

            _dbContext = new ApplicationDbContext(dbContextOptionBuilder.Options);

            //Seed data into context
            _dbContext.Add(
                            new Order
                            {
                                OrderNumber = 17835,
                                OrderLineNumber = 1,
                                ProductNumber = "123 - 100",
                                Quantity = 75,
                                Name = "Little Guys"
                            ,
                                Description = "Small Guys",
                                Price = 0.99,
                                ProductGroup = "Normal",
                                OrderDate = DateTime.Parse("2014-07-05".Trim(), NumberFormatInfo.InvariantInfo),
                                CustomerName = "Kalle Svensson",
                                CustomerNumber = 265849
                            });
            _dbContext.Add(
                            new Order
                            {
                                OrderNumber = 17835,
                                OrderLineNumber = 2,
                                ProductNumber = "123 - 200",
                                Quantity = 1,
                                Name = "Normal Guys"
                            ,
                                Description = "Extremely Normal Guys",
                                Price = 0.49,
                                ProductGroup = "Normal",
                                OrderDate = DateTime.Parse("2014-07-07".Trim(), NumberFormatInfo.InvariantInfo),
                                CustomerName = "Kalle Svensson",
                                CustomerNumber = 265849
                            });
            _dbContext.SaveChanges();

            //Set up options
            options = new ImporterOptions();
            options.Directory = "App_Data";
            options.Pattern = "Order*";

            _importerService = new ImporterService(_dbContext, options);
        }

        [TestMethod]
        public async Task LoadFiles_NoFileLoaded_ReturnEmptyList()
        {
            //Arrange
            int numberOfElementsInList = 0;
            var mockTxtHandler = new Mock<TxtHandler>();
            mockTxtHandler.CallBase = true;
            mockTxtHandler.Setup(x => x.LoadFiles(options.Directory, options.Pattern)).Returns(new List<Order>());

            //Act
            var newOrders = await _importerService.LoadFiles(mockTxtHandler.Object);

            //Assert     
            Assert.AreEqual(numberOfElementsInList, newOrders.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            IEnumerable<Order> orders = _dbContext.Order;

            foreach (var order in orders)
            {
                _dbContext.Remove(order);
            }

            _dbContext.SaveChanges();
        }
    }
}

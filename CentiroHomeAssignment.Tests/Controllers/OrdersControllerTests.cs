using CentiroHomeAssignment.Controllers;
using CentiroHomeAssignment.Data;
using CentiroHomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CentiroHomeAssignment.Tests
{

    [TestClass]
    public class OrdersControllerTests
    {
        private readonly OrdersController _ordersController;
        private ApplicationDbContext _dbContext;

        //[TestInitialize]
        public OrdersControllerTests()
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

            //Simulate appSettings to pass has argument
            var appSettings = @"{""Import"":{
            ""DirectoryPath"" : ""App_Data"",
            ""Pattern"" : ""Order*""
            }}";

            var builder = new ConfigurationBuilder();

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));

            var configuration = builder.Build();

            _ordersController = new OrdersController(_dbContext, configuration);
        }

        [TestMethod]
        public void GetAll_ActionExecutes_ReturnsNumberOfOrders()
        {
            //Arrange
            IEnumerable<Order> orders = _dbContext.Order;

            //Act
            var result = _ordersController.GetAll();

            var ordersResult = result as ViewResult;
            //Assert
            Assert.AreEqual(orders, ordersResult.Model);
        }

        [TestMethod]
        public void GetByOrderNumber_IdIsZero_ReturnsNotFound()
        {
            //Arrange
            int id = 0;

            //Act
            var result = _ordersController.GetByOrderNumber(id);

            var ordersResult = result as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        /* -> Id can be changed. It depends on the database and status
        [TestMethod]
        public void GetByOrderNumber_GetOrders_ReturnsListOfOrdersWithSameNumber()
        {
            //Arrange
            int id = 1;
            int orderNumber = 17835;

            //Act
            var result = _ordersController.GetByOrderNumber(id);

            var ordersResult = result as ViewResult;
            var ordersIEnumerable = ordersResult.Model as IEnumerable<Order>;

            //Assert     
            foreach (var ord in ordersIEnumerable)
            {
                Assert.AreEqual(orderNumber, ord.OrderNumber);
            }
        }
        */

        [TestCleanup]
        public void Cleanup()
        {
            IEnumerable<Order> orders = _dbContext.Order;

            foreach(var order in orders)
            {
                _dbContext.Remove(order);
            }

            _dbContext.SaveChanges();
        }
    }
}

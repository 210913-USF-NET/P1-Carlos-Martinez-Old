using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Xunit;
using DL;
using Models;

namespace Tests
{
    public class ModelTests
    {
        [Fact]
        public void testCustomerName()
        {
            // Arrange
            Customer test = new Customer();
            string customerName = "Rachel";

            // Act
            test.Name = customerName;

            // Assert
            Assert.Equal(customerName, test.Name);
        }

        [Fact]
        public void testCustomerCredit()
        {
            // Arrange
            Customer test = new Customer();

            // Act
            // nothing

            // Assert
            Assert.Equal(30, test.Credit);
        }

        [Fact]
        public void testInventoryQuantity()
        {
            // Arrange
            Inventory test = new Inventory();

            // Act
            test.Quantity = 20;

            // Assert
            Assert.Equal(20, test.Quantity);
        }

        [Fact]
        public void testLineItemQuantity()
        {
            // Arrange
            LineItem test = new LineItem();

            // Act
            test.Quantity = 20;

            // Assert
            Assert.Equal(20, test.Quantity);
        }

        [Fact]
        public void testOrdersStoreFrontId()
        {
            // Arrange
            Orders test = new Orders();

            // Act
            test.StoreFrontId = 3;

            // Assert
            Assert.Equal(3, test.StoreFrontId);
        }

        [Fact]
        public void testOrdersCustomerId()
        {
            // Arrange
            Orders test = new Orders();

            // Act
            test.CustomerId = 3;

            // Assert
            Assert.Equal(3, test.CustomerId);
        }
        
        [Fact]
        public void testOrdersTest()
        {
            // Arrange
            Orders test = new Orders();

            // Act
            test.Total = 10;

            // Assert
            Assert.Equal(10, test.Total);
        }
        
        [Fact]
        public void testStoreFrontName()
        {
            // Arrange
            StoreFront test = new StoreFront();
            string name = "Heya";

            // Act
            test.Name = name;

            // Assert
            Assert.Equal(name, test.Name);
        }
        
        [Fact]
        public void testProductName()
        {
            // Arrange
            Product test = new Product();
            string name = "Raptor!";

            // Act
            test.Name = name;

            // Assert
            Assert.Equal(name, test.Name);
        }
        
        [Fact]
        public void testProductPrice()
        {
            // Arrange
            Product test = new Product();

            // Act
            test.Price = 5;

            // Assert
            Assert.Equal(5, test.Price);
        }
    }
}

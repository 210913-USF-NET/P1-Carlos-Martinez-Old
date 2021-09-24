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
        public void Template()
        {
            // Arrange
            IRepo repo = new DBRepo(context);

            // Act
            var customers = repo.GetAllCustomers();

            // Assert
            Assert.Equal(2, customers.Count);
        }
    }
}

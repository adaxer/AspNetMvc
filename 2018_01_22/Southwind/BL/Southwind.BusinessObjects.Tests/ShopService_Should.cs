using NUnit.Framework;
using Southwind.Interfaces;
using Southwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.BusinessObjects.Tests
{
    [TestFixture]
    public class ShopService_Should
    {
        [Test]
        public void Return_List_Of_Category()
        {
            // Arrange
            var sut = new ShopService(null);

            // Act
            var result = sut.GetCategories().Result;

            // Assert
            Assert.NotNull(result, "ShopService must not return null");
            Assert.IsInstanceOf<IEnumerable<Category>>(result, "ShopService must return IEnumerable<Category>");
        }
    }
}

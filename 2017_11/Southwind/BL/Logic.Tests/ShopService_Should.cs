using Moq;
using NUnit.Framework;
using Southwind.Contracts.Interfaces;
using Southwind.Contracts.Models;
using Southwind.Logic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Logic.Tests
{
    [TestFixture]
    public class ShopService_Should
    {
        [Test]
        public void Return_NotNull_List_Of_Categories()
        {
            // Arrange
            var mock = new Mock<IRepository<Category>>();
            var dummyRepository = mock.Object;
            var service = new ShopService(dummyRepository);

            // Act
            var result = service.GetCategoriesAsync().Result;
            Console.WriteLine("Mal sehen");

            // Assert
            Assert.IsNotNull(result, "Ergebnis darf nicht Null sein");
            Assert.IsInstanceOf(typeof(List<Category>), result, "Ergebnis muss Liste von Category sein");
        }

        [Test]
        public void Use_Given_Repository()
        {
            // Arrange
            var mock = new Mock<IRepository<Category>>();
            var dummyRepository = mock.Object;
            var service = new ShopService(dummyRepository);

            // Act
            var result = service.GetCategoriesAsync().Result;
            Console.WriteLine("Mal sehen");

            // Assert
            mock.Verify(rc => rc.Find(It.IsAny<Expression<Func<Category, bool>>>()), Times.AtLeastOnce(), "Repository wurde nicht verwendet");
        }

        [Test]
        public void Return_Result_From_Repository()
        {
            // Arrange
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(rc => rc.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(new List<Category>
                {
                    new Category { CategoryId=1, CategoryName="Kategorie 1"},
                    new Category { CategoryId=2, CategoryName="Kategorie 2"}
                }.AsQueryable());
            var dummyRepository = mock.Object;
            var service = new ShopService(dummyRepository);

            // Act
            var result = service.GetCategoriesAsync().Result;
            Console.WriteLine("Mal sehen");

            // Assert
            Assert.IsTrue(result.Count() == 2, "Falsche Anzahl im Result");
            Assert.AreEqual(result.First().CategoryName, "Kategorie 1", "Daten stimmen nicht überein");
            Assert.AreEqual(result.Skip(1).First().CategoryName, "Kategorie 2", "Daten stimmen nicht überein");
        }
    }
}

using NUnit.Framework;
using Southwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.DataAccess.Tests
{
    [TestFixture]
    public class Repository_Should
    {
        private SouthwindDb _db;
        private GenericRepository<Category> _repository;

        [Test]
        public void Read_From_Categories()
        {
            // Arrange
            var db = new SouthwindDb();
            db.Database.Log = Console.WriteLine;
            var repo = new GenericRepository<Category>(db);

            // Act
            var cats = repo.Find();
            Console.WriteLine("After Find");

            // Assert
            Assert.True(cats != null && cats.Count() > 0, "Category table must contain data, Repo must find them");
        }

        [Test]
        public void Not_Delete_DbContext_When_Done()
        {
            // Arrange
            var db = new SouthwindDb();

            // Act
            var cats = UseRepository(db);

            // Assert
            Assert.DoesNotThrow(() => cats.Count(), "Should not throw");
        }

        private IQueryable<Category> UseRepository(SouthwindDb db)
        {
            var repo = new GenericRepository<Category>(db);
            return repo.Find();
        }

        [Test]
        public void Successfully_Write_Data()
        {
            // Arrange
            var cat = _repository.Find().First();
            var before = cat.CategoryName;

            // Act
            var after = before + ".";
            cat.CategoryName = after;
            _db.SaveChanges();
            Init();

            // Assert
            cat = _repository.Find().First();
            Assert.AreEqual(cat.CategoryName, after, "Value not saved");

            // Cleanup (besser: TearDown nach jedem Test, DataBase inmemory)
            cat.CategoryName = before;
            _db.SaveChanges();
        }

        [SetUp]
        public void Init()
        {
            _db = new SouthwindDb();
            _repository = new GenericRepository<Category>(_db);
        }
    }
}

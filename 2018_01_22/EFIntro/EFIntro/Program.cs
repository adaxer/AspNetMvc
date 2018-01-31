using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<LibDb>(new DropCreateDatabaseIfModelChanges<LibDb>());

            LibDb db = new LibDb();
            db.Database.Log = new Action<string>(Console.WriteLine);
            //RefillDataBase(db);

            // var people = db.People.ToList();
            var books = db.Books.ToList();
            var author = books.First().Author;
        }

        private static void RefillDataBase(LibDb db)
        {
            db.Database.ExecuteSqlCommand("Truncate Table Books");
            db.Database.ExecuteSqlCommand("Delete from People");

            var book = new Book { Title = "Harry Potter" };
            var author = new Person { Name = "J.K. Rowling" };

            author.WrittenBooks.Add(book);
            db.People.Add(author);

            book = new Book { Title = "Herr der Ringe", Author = new Person { Name = "J.R.R. Tolkien" } };
            db.Books.Add(book);

            db.SaveChanges();
        }
    }

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Published { get; set; }

        public virtual Person Author { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Book> WrittenBooks { get; set; } = new List<Book>();
    }

    public class LibDb : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Person> People { get; set; }
    }
}

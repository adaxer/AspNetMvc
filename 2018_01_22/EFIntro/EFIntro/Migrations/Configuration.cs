namespace EFIntro.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFIntro.LibDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EFIntro.LibDb";
        }

        protected override void Seed(EFIntro.LibDb db)
        {
            var book = new Book { Title = "Harry Potter" };
            var author = new Person { Name = "J.K. Rowling" };

            author.WrittenBooks.Add(book);
            db.People.Add(author);

            book = new Book { Title = "Herr der Ringe", Author = new Person { Name = "J.R.R. Tolkien" } };
            db.Books.Add(book);
        }
    }
}

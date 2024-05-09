using Microsoft.EntityFrameworkCore;
// ReSharper disable all
namespace Many_To_Many_Relationships;

class Program
{
   


    static void Main(string[] args)
    {

        
    }



}

#region Default Convention

//public class Author
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Book> Books { get; set; } = new List<Book>();
//}

//public class Book
//{

//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public int PageSize { get; set; }

//    public ICollection<Author> Authors { get; set; } = new List<Author>();
//}

#endregion

#region Data Annotations

// Cross Table manuel oluşturulmak zorunda
//public class Author
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<AuthorBook>? AuthorBooks { get; set; }
//}

//public class Book
//{

//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public int PageSize { get; set; }

//    public ICollection<AuthorBook>? AuthorBooks { get; set; }

//}

//public class AuthorBook
//{
//    [Key]
//    public int AuthorId { get; set; }
//    public int BookId { get; set; }

//    public Author? Author { get; set; }
//    public Book? Book { get; set; }
//}

//public class LibraryDbContext : DbContext
//{

//    public DbSet<Author> Authors { get; set; }
//    public DbSet<Book> Books { get; set; }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
//    }


//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<AuthorBook>().HasKey(x => new
//        {
//            x.AuthorId,
//            x.BookId,
//        });
//    }
//}

#endregion

#region Fluent Api

// Cross table oluşturulmalı 


public class Author
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<AuthorBook>? AuthorBooks { get; set; }
}

public class Book
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PageSize { get; set; }

    public ICollection<AuthorBook>? AuthorBooks { get; set; }

}

public class AuthorBook
{

    public int AuthorId { get; set; }
    public int BookId { get; set; }

    public Author? Author { get; set; }
    public Book? Book { get; set; }
}

public class LibraryDbContext : DbContext
{

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>().HasKey(x => new
        {
            x.AuthorId,
            x.BookId,
        });


        modelBuilder.Entity<AuthorBook>()
            .HasOne(x => x.Book)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.BookId);


        modelBuilder.Entity<AuthorBook>()
            .HasOne(x => x.Author)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.AuthorId);
    }
}


#endregion




////public class LibraryDbContext : DbContext
////{

////    public DbSet<Author> Authors { get; set; }
////    public DbSet<Book> Books { get; set; }


////    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////    {
////        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
////    }
////}

//public class Author
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Book> Books { get; set; } = new List<Book>();
//}

//public class Book
//{

//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public int PageSize { get; set; }

//    public ICollection<Author> Authors { get; set; } = new List<Author>();
//}
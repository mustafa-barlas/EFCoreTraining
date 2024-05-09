using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
// ReSharper disable all


ApplicationContext context = new ApplicationContext();



#region OneToOne

var person = await context.Persons.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id.Equals(22));

context.Addresses.Remove(person.Address);

await context.SaveChangesAsync();

#endregion

#region OneToMany

var blog = await context.Blogs.Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id.Equals(55));

var post = blog.Posts.FirstOrDefault(x => x.Id.Equals(55));

context.Posts.Remove(post);
await context.SaveChangesAsync();

#endregion

#region ManyToMany

var book = context.BookFors.Include(x => x.AuthorFors).FirstOrDefault(x => x.Id.Equals(3));

var author = book.AuthorFors.FirstOrDefault(x => x.Id.Equals(3));
// context.AuthorForDefaults.Remove(author); // Bu Kod Yazarı silmeye çalışır

book.AuthorFors.Remove(author);
context.SaveChanges();



#endregion



public class AuthorFor
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<BookFor>? BookFors { get; set; }
}
public class BookFor
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PageSize { get; set; }

    public List<AuthorFor?>? AuthorFors { get; set; }

}
public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Address? Address { get; set; }
}
public class Address
{
    public int Id { get; set; }
    public string? Text { get; set; }

    public Person? Person { get; set; }
}
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
public class Blog
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Post>? Posts { get; set; } = new HashSet<Post>();
}
public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }
}

public class ApplicationContext : DbContext
{

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<AuthorFor> AuthorFors { get; set; }
    public DbSet<BookFor> BookFors { get; set; }


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



        modelBuilder.Entity<AuthorBook>()   //  Many To Many
            .HasOne(x => x.Book)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.BookId);


        modelBuilder.Entity<AuthorBook>() // Many To Many
            .HasOne(x => x.Author)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.AuthorId);



        modelBuilder.Entity<Address>() //  One To Many
            .HasOne(x => x.Person)
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(x => x.Id);
        //.OnDelete(DeleteBehavior.Cascade);
        //.OnDelete(DeleteBehavior.SetNull);
        //.OnDelete(DeleteBehavior.Restrict);
    }


    #region Cascade

    // Principle tablodan silinen veriyle bağımlı olan veriyi de beraberinde siler

    #endregion

    #region SetNull
    // Principle tablodan silinen veriyle bağımlı olan veriyi null değerni atar


    #endregion

    #region Restrict

    // Silinmeye çalışılan veriye bağımlı bir veri var ise silme işlemini engeler

    #endregion
}
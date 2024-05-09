using Microsoft.EntityFrameworkCore;
// ReSharper disable all
ApplicationContext context = new ApplicationContext();



#region OneToOne

#region Principal  üzerinden dependent ekleme

Person? person = await context.Persons.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id.Equals(25));

context.Addresses.Remove(person.Address);
person.Address = new Address() { Text = "New York sokağı" };

await context.SaveChangesAsync();

#endregion

#region Dependent

Address? address = await context.Addresses.FindAsync(52);

context.Addresses.Remove(address);
await context.SaveChangesAsync();

Person? person2 = await context.Persons.FindAsync(23);
address.Person = person2;
await context.Addresses.AddAsync(address);
// OR 



#endregion

#endregion

#region OneToMany

#region Principal üzerinden dependent günceleme

Blog? blog = await context.Blogs.Include(x => x.Posts).SingleOrDefaultAsync(x => x.Id.Equals(22));

Post? post = blog.Posts.FirstOrDefault(x => x.Id.Equals(26));
blog.Posts.Remove(post);

blog.Posts.Add(new Post() { Title = "Post 20" });
blog.Posts.Add(new Post() { Title = "Post 48" });

await context.SaveChangesAsync();

#endregion

#region Dependent üzerinden principal güncelleme

Post? post53 = await context.Posts.FindAsync(53);

post53.Blog = new Blog()
{
    Name = "Blog47"
};

await context.SaveChangesAsync();

Blog? blog47 = await context.Blogs.FindAsync(47);

post53.Blog = blog;

await context.SaveChangesAsync();


#endregion


#endregion

#region ManyToMany

#region 1. Örnek

BookForDefault? bookForDefault = await context.BookForDefaults.FindAsync(55);
AuthorForDefault? authorForDefault = await context.AuthorForDefaults.FindAsync(65);

bookForDefault.AuthorForDefaults.Add(authorForDefault);

await context.SaveChangesAsync();


#endregion

#region 1. Örnek

AuthorForDefault? author = await context.AuthorForDefaults.Include(x => x.BookForDefaults).SingleOrDefaultAsync(x => x.Id.Equals(55));


foreach (var book in author.BookForDefaults)
{
    if (book.Id != 1)
    {
        author.BookForDefaults.Remove(book);
    }
}

await context.SaveChangesAsync();

#endregion

#region 1. Örnek

BookForDefault? boook = await context.BookForDefaults.Include(x => x.AuthorForDefaults)
    .FirstOrDefaultAsync(x => x.Id.Equals(5));




AuthorForDefault authorFor = boook.AuthorForDefaults.FirstOrDefault(x => x.Id.Equals(33));
boook.AuthorForDefaults.Remove(authorFor);

AuthorForDefault? author1 = await context.AuthorForDefaults.FirstOrDefaultAsync(x => x.Id.Equals(556));

boook.AuthorForDefaults.Add(author1);
boook.AuthorForDefaults.Add(new()
{
    Name = "Burak"
});

await context.SaveChangesAsync();

#endregion

#endregion



public class AuthorForDefault
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<BookForDefault>? BookForDefaults { get; set; }
}
public class BookForDefault
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PageSize { get; set; }

    public List<AuthorForDefault?>? AuthorForDefaults { get; set; }

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
    public DbSet<AuthorForDefault> AuthorForDefaults { get; set; }
    public DbSet<BookForDefault> BookForDefaults { get; set; }


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



        modelBuilder.Entity<Address>()  //  One To Many
            .HasOne(x => x.Person)
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(x => x.Id);
    }
}

using Microsoft.EntityFrameworkCore;
// ReSharper disable all
ApplicationContext context = new ApplicationContext();



#region OneToOne


#region Principal  üzerinden dependent ekleme

Person person = new Person();
person.Name = "Ceyda";
person.Address = new Address() { Text = "" };

await context.AddAsync(person);
await context.SaveChangesAsync();

#endregion

#region Dependent

Address address = new Address()
{
    Text = "Colorado mahallesi abc sokak",
    Person = new Person()
    {
        Name = "Burak"
    }
};

await context.AddAsync(address);
await context.SaveChangesAsync();

#endregion

#endregion

#region OneToMany

#region Principal üzerinden dependent ekleme

#region Nesne referansı üzerinden ekleme


Blog blog = new Blog() { Name = "Blog 1" };
blog.Posts.Add(new Post() { Title = "Post 1" });
blog.Posts.Add(new Post() { Title = "Post 2" });
blog.Posts.Add(new Post() { Title = "Post 3" });
blog.Posts.Add(new Post() { Title = "Post 4" });

await context.Blogs.AddAsync(blog);
await context.SaveChangesAsync();

#endregion

#region Object inializer üzerinden ekleme


Blog blog2 = new Blog()
{
    Name = "Blog 2",
    Posts = new List<Post>()
    {
        new Post() { Title = "Post 1" },
        new Post() { Title = "Post 2" },
        new Post() { Title = "Post 3" },
    }
};

await context.Blogs.AddAsync(blog2);
await context.SaveChangesAsync();

#endregion

#endregion

#region Dependent üzerinden principal ekleme

Post post1 = new Post()
{
    Title = "Post 1",
    Blog = new Blog()
    {
        Name = "Blog 1"
    }
};



await context.Posts.AddAsync(post1);
await context.SaveChangesAsync();

#endregion

#region ForeignKey üzerinden ekleme

Post post2 = new Post()
{
    Title = "Post 1",
    BlogId = 1
};

await context.Posts.AddAsync(post2);
await context.SaveChangesAsync();

#endregion

#endregion

#region ManyToMany

#region 1. Yöntem

// N To N ilişkisi default convention ile tasarlanmışsa 

BookForDefault book = new BookForDefault()
{
    Name = "A Book",
    Authors = new List<Author>()
    {
        new Author() { Name = "Mustafa" },
        new Author() { Name = "Ceyda" },
    }
};

await context.BookForDefaults.AddAsync(book);
await context.SaveChangesAsync();



#endregion

#region 2. Yöntem
// N To N ilişkisi Fluent Api ile tasarlanmışsa 

Author author = new Author()
{
    Name = "Mustafa",
    AuthorBooks = new List<AuthorBook>()
    {
        new AuthorBook(){BookId = 1},
        new AuthorBook(){Book = new Book()
            {
                Name = "Book A"
            }

        },
    }
};

await context.Authors.AddAsync(author);
await context.SaveChangesAsync();

#endregion
#endregion



public class AuthorForDefault
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Book>? Books { get; set; }
}
public class BookForDefault
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int PageSize { get; set; }

    public ICollection<Author>? Authors { get; set; }

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
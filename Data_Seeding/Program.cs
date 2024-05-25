using Microsoft.EntityFrameworkCore;
//Resharper disable all


Console.WriteLine();

#region Data Ekleme
//public class ApplicationContext1 : DbContext
//{


//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Blog>().HasData
//        (
//            new Blog() { Id = 5, Name = "acasca" },
//            new Blog() { Id = 52, Name = "casccascaasav" }
//        );

//        modelBuilder.Entity<Post>().HasData
//        (
//            new Post() { Id = 12, Title = "Post 4", Content = "ssvf", BlogId = 5 },
//            new Post() { Id = 42, Title = "Post 6", Content = "qwertyjk", BlogId = 52 }

//        );
//    }
//}


#endregion


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
    public string? Content { get; set; }

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }
}

public class ApplicationContext : DbContext
{


    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

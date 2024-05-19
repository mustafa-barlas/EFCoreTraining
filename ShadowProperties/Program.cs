using Microsoft.EntityFrameworkCore;
// resharper disable all
ApplicationContext context = new ApplicationContext();



#region Shadow Properties nedir?

// Entity sınıfarında fiziksel olarak tanımlanmayan ancak efcore tarafından ilgili entity için var oldugu kabul edilen propertylerdir.
// tabloda gösterilmesini istemediğimiz ve üzerinde işlem yapmayacagımız kolonlar 
#endregion

#region Foreign Key

// biz eklemesek de efcore post entity sine blogId yi kendisi shadow propery olarak ekler.
var blogs = await context.Blogs.Include(x => x.Posts).ToListAsync();

#endregion

#region Shadow Property oluşturma
// Fluent api kulanılmalı
//public class ApplicationContext1 : DbContext
//{
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
//    }


//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Blog>()
//            .Property<DateTime>("CreatedDate");
//    }
//}

#endregion

#region Shadow propertye erişim sağlama

#region ChangeTracker ile erişim sağlama

var blog = await context.Blogs.FirstAsync();

var createdDate = context.Entry(blog).Property("CreatedDate");

Console.WriteLine(createdDate.CurrentValue);
Console.WriteLine(createdDate.OriginalValue);


createdDate.CurrentValue = DateTime.UtcNow;
await context.SaveChangesAsync();

#endregion

#region Ef.Property ile erişim sağlama

var blogss = await context.Blogs.OrderBy(x => EF.Property<DateTime>(x, "CreatedDate")).ToListAsync();


var blog2s = await context.Blogs.Where(x => EF.Property<DateTime>(x, "CreatedDate").Year > 2000).ToListAsync();



#endregion

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
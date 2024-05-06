using Microsoft.EntityFrameworkCore;
// ReSharper disable all


LibrarDbContext context = new LibrarDbContext();




#region AsNoTracking Metodu  

// ilişkisel verilerde maliyetlidir. çünkü yinelenen verileri farklı instance lerde tutar. ama takip edildiinde aynı verileri aynı instance lerde saklar. ilişkisel veri yok ise

var authors = await context.Authors.Include(x => x.Books).AsNoTracking().ToListAsync();


#endregion

#region  AsNoTrackingWithIdentityResolution

// AsNoTracking deki instance problemini gidermek için kullanılır. ilişkisel veri var ise
authors = await context.Authors.Include(x => x.Books).AsNoTrackingWithIdentityResolution().ToListAsync();


#endregion

#region AsTracking



#endregion

#region UseQueryTrackingBehavior

//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
//    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
//    optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
//}

#endregion



public class LibrarDbContext : DbContext
{

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

public class Author
{
    public Author()
    {
        Console.WriteLine("Authorssssssss");
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Book
{
    public Book()
    {
        Console.WriteLine("Bookssssss");
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int PageSize { get; set; }

    public ICollection<Author> Authors { get; set; } = new List<Author>();
}
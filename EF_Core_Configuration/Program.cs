using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Resharper disable all


//ApplicationContext context = new ApplicationContext();



#region OnModelCreating Metodu

#region GetEntityTypes

public class ApplicationContext1 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entites = modelBuilder.Model.GetEntityTypes().ToList();
    }
}

#endregion



#endregion

#region Configurations | Data Annotations And Fluent Api

#region Table - ToTable
// Generate edilecek tablonun adını belirlememizi sağlar

[Table("People")]
public class Person1
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Address? Address { get; set; }
}


public class ApplicationContext2 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person1>().ToTable("People");
    }
}

#endregion

#region Column - HasColumnName, HasColumnType, HasColumnOrder


public class Person2
{
    public int Id { get; set; }
    [Column("Firstname", TypeName = "text")]
    public string? Name { get; set; }

}

public class ApplicationContext3 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person2>().Property(x => x.Name).HasColumnName("Firstname").HasColumnType("text").HasColumnOrder(1);
    }
}

#endregion

#region ForeignKey - HasForeignKey


public class Person3
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [ForeignKey(nameof(Department))]
    public int DepartId { get; set; }
    public Department? Department { get; set; }

}

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Person3>? Person3s { get; set; }
}

public class ApplicationContext03 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person3>()
            .HasOne(x => x.Department)
            .WithMany(x => x.Person3s)
            .HasForeignKey(x => x.DepartId);
    }
}

#endregion

#region NotMapped - Ignore

// Ef CORE Entity sınıfları içerisindeki tüm property leri kolon olarak migrate eder. eğer propertyin migrate edilmesini  istemiyorsak 

public class Person4
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [NotMapped]
    public string? BlaBla { get; set; }
}


public class ApplicationContext4 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person4>().Ignore(x => x.BlaBla);
    }
}

#endregion

#region Key - HasKey

public class Person5
{
    [Key]
    public int Cevdet { get; set; } // ID
    public string? Name { get; set; }

}

public class ApplicationContext5 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person5>().HasKey(x => x.Cevdet); // ID
    }
}

#endregion

#region HasNoKey

public class Person7
{
    public string? Cevdet { get; set; }
    public string? Name { get; set; }

}


public class ApplicationContext7 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person7>().HasNoKey();
    }
}

#endregion

#region Timestamp - IsRowVersion

// verilerin versiyonu ile ilgili çalışmalar için kullanılır. veri tutarlılıgı

public class Person6
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}


public class ApplicationContext6 : DbContext
{
    DbSet<Person6> Person6s { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person6>().Property(x => x.RowVersion).IsRowVersion();
    }
}

#endregion

#region Required - IsRequired

public class Person8
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

}


public class ApplicationContext8 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person8>().Property(x => x.Name).IsRequired();
    }
}

#endregion

#region MaxLenght - HasMaxLenght



#endregion

#region MinLenght - HasMinLenght


#endregion

#region Precision - HasPrescision

//   Küsüratlı sayılarda noktadan sonra kaç basamak olacagını belirlemeizi sağlar.

public class Person9
{
    public int Id { get; set; }

    public string? Name { get; set; }
    [Precision(8, 7)]  // 1.2345678
    public float Price { get; set; }

}


public class ApplicationContext9 : DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person9>().Property(x => x.Price).HasPrecision(6, 4); // 56.2564
    }
}

#endregion

#region Unicode - IsUnicode

public class Person10
{
    public int Id { get; set; }

    public string? Name { get; set; }
    [Unicode]
    public string? Email { get; set; }


}


public class ApplicationContext10 : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person10>().Property(x => x.Email).IsUnicode();
    }
}

#endregion

#region Comment - HasComment

public class Person11
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [Comment("This is nothing ")]
    public string? Detail { get; set; }

}


public class ApplicationContext11 : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person11>().Property(x => x.Detail).HasComment("Not important thing");
    }
}

#endregion

#region ConcurrencyCheck -  IsConcurrencyCheck

// veri bütünlüğünü sağlayacak token yapılanması

public class Person12
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}


public class ApplicationContext12 : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person12>().Property(x => x.RowVersion).IsConcurrencyToken();
    }
}

#endregion

#region InverseProperty

public class Flight
{
    public int FlightId { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public string? Name { get; set; }

    public Airport? DepartureAirport { get; set; }
    public Airport? ArrivalAirport { get; set; }

}

public class Airport
{
    public int AirportId { get; set; }
    public string? Name { get; set; }

    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight>? DepartingFlights { get; set; }

    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight>? ArivingFlights { get; set; }
}


public class ApplicationContext13 : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}

#endregion

#endregion



//public class AuthorForDefault
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Book>? Books { get; set; }
//}
//public class BookForDefault
//{

//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public int PageSize { get; set; }

//    public ICollection<Author>? Authors { get; set; }

//}
//public class Person
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public Address? Address { get; set; }
//}
//public class Address
//{
//    public int Id { get; set; }
//    public string? Text { get; set; }

//    public Person? Person { get; set; }
//}
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

//    public int AuthorId { get; set; }
//    public int BookId { get; set; }

//    public Author? Author { get; set; }
//    public Book? Book { get; set; }
//}
//public class Blog
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Post>? Posts { get; set; } = new HashSet<Post>();
//}
//public class Post
//{
//    public int Id { get; set; }
//    public string? Title { get; set; }

//    public int BlogId { get; set; }
//    public Blog? Blog { get; set; }
//}

//public class ApplicationContext : DbContext
//{

//    public DbSet<Author> Authors { get; set; }
//    public DbSet<Book> Books { get; set; }
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }
//    public DbSet<AuthorForDefault> AuthorForDefaults { get; set; }
//    public DbSet<BookForDefault> BookForDefaults { get; set; }


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



//        modelBuilder.Entity<AuthorBook>()   //  Many To Many
//            .HasOne(x => x.Book)
//            .WithMany(x => x.AuthorBooks)
//            .HasForeignKey(x => x.BookId);


//        modelBuilder.Entity<AuthorBook>() // Many To Many
//            .HasOne(x => x.Author)
//            .WithMany(x => x.AuthorBooks)
//            .HasForeignKey(x => x.AuthorId);



//        modelBuilder.Entity<Address>()  //  One To Many
//            .HasOne(x => x.Person)
//            .WithOne(x => x.Address)
//            .HasForeignKey<Address>(x => x.Id);
//    }
//}
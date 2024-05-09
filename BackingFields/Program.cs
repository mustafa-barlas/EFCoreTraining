using Microsoft.EntityFrameworkCore;
//ReSharper disable all

ApplicationContext context = new ApplicationContext();


#region Backing Fields

// entity lermizde property  ile db de kolonlara property ler değil de field ler göndermemizi sağlar

#endregion

#region Backing fields attribute

public class Person1
{
    public int Id { get; set; }

    public string? name;
    [BackingField(nameof(name))]
    public string? Name { get; set; }
}

#endregion

#region HasField Fluent Api

public class ApplicationContext1 : DbContext
{

    public DbSet<Person> Persons { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().Property(x => x.Name).HasField(nameof(Person.Name));
    }
}

#endregion

#region Field and property Access

public class ApplicationContext2 : DbContext
{

    public DbSet<Person> Persons { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(x => x.Name)
            .HasField(nameof(Person.Name))
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

#endregion

#region Field-Only Properties



#endregion



public class Person
{
    public int Id { get; set; }

    public string? name;
    public string? Name { get => name; set => name = value; }
}

public class ApplicationContext : DbContext
{

    public DbSet<Person> Persons { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }

}
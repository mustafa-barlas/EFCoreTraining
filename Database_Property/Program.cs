using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

//resharper disable all
Console.WriteLine("Hello, World!");

ApplicationDbContext context = new ApplicationDbContext();

#region Database Property si

#region BeginTransaction

IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

#endregion

#region CommitTransaction

await context.Database.CommitTransactionAsync();

#endregion


















#endregion


public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public Person Person { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    // public DbSet<PersonOrder> PersonOrders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}
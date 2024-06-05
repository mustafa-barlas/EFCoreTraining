using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// resharper disable all
Console.WriteLine("Hello, World!");
ApplicationDbContext context = new ApplicationDbContext();

#region Loglama

var datas = context.Persons.ToList();

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

    public Person Person { get; set; }
}

public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    // private StreamWriter _log = new StreamWriter("log.txt", append: true);

    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
        builder.AddFilter((category, level) =>
        {
            return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
        });
    });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");

        optionsBuilder.UseLoggerFactory(_loggerFactory); // ***********

        // optionsBuilder.LogTo(message => _log.WriteLine(message),LogLevel.Error)
        //     .EnableSensitiveDataLogging().EnableDetailedErrors();
    }

    // public override void Dispose()
    // {
    //     base.Dispose();
    //     _log.Dispose();
    // }
    //
    // public async override ValueTask DisposeAsync()
    // {
    //     await _log.DisposeAsync();
    //     await base.DisposeAsync();
    // }
}
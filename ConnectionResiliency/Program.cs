// resharper disable all

using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

Console.WriteLine("Hello, World!");
ApplicationContext1 context1 = new ApplicationContext1();

#region EnableRetryOnFailure

// while (true)
// {
//     await Task.Delay(2000);
//     var persons = await context1.Persons.ToListAsync();
//     foreach (var person in persons)
//     {
//         Console.WriteLine(person.Name);
//     }
//
//     Console.WriteLine("-------------------------------------------------------");
// }


// public class ApplicationContext1 : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;",
//             builder => builder.EnableRetryOnFailure(
//                 maxRetryCount: 6,
//                 maxRetryDelay: TimeSpan.FromSeconds(15),
//                 errorNumbersToAdd: new[] { 4060 }
//             )); // -*-***//*///-/**/*/*+
//     }
// }

#endregion

#region Execution Strategies

class CustomExecutionStrategys : ExecutionStrategy
{
    public CustomExecutionStrategys(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context,
        maxRetryCount, maxRetryDelay)
    {
    }

    public CustomExecutionStrategys(ExecutionStrategyDependencies dependencies, int maxRetryCount,
        TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
    {
    }

    private int retryCount = 0;

    protected override bool ShouldRetryOn(Exception exception)
    {
        Console.WriteLine($"{++retryCount}. Bağlantı yendiden kuruluyor");
        return true;
    }
}


public class ApplicationContext1 : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;",
            builder =>
                builder.ExecutionStrategy(
                    dependencies => new CustomExecutionStrategys(
                        dependencies,
                        3,
                        TimeSpan.FromSeconds(15))));
    }
}

#endregion

#region Bağlantı koptugunda işlemlere devam ettirmek

// var strategy = context1.Database.CreateExecutionStrategy();
// await strategy.ExecuteAsync(async () =>
// {
//     using var transaction = await context1.Database.BeginTransactionAsync() ;
//     await context1.Persons.AddAsync(new()
//         { Name = "ceyda", Surname = "Kalkancı", DateOfBirth = new DateTime(2012, 12, 12) });
//     await context1.SaveChangesAsync();
//
//     await transaction.CommitAsync();
// });

// public class ApplicationContext1 : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;",
//             builder =>
//                 builder.ExecutionStrategy(
//                     dependencies => new CustomExecutionStrategys(
//                         dependencies,
//                         3,
//                         TimeSpan.FromSeconds(15))));
//     }
// }

#endregion

public class Person
{
    public int PersonId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
//resharper disable all

ApplicationDbContext context = new ApplicationDbContext();
Console.WriteLine();

#region View

#region View Oluştrma

// 1. boş bir view oluşturlmalı
// 1. up fonksiyonunda migration yazılmaıl

// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql(
//         $@"CREATE VIEW VM_PERSONORDERS 
// AS
// SELECT P.Name, COUNT(*) [COUNT] FROM Persons P
// INNER JOIN  Orders O 
// ON P.PersonId = O.PersonId
// GROUP BY P.Name
// ");
// }
//
// /// <inheritdoc />
// protected override void Down(MigrationBuilder migrationBuilder)
// {
//
// }

#endregion

#region View'i DbSet olarak ayarlama

var result = await context.PersonOrders.Where(x => x.Count > 5).ToListAsync();
foreach (var item in result)
{
    Console.WriteLine(item.Name);
    Console.WriteLine(item.Count);
}

// class ApplicationDbContext1 : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//     public DbSet<Order> Orders { get; set; }
//     public DbSet<PersonOrder> PersonOrders { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//
//         modelBuilder.Entity<PersonOrder>()
//             .ToView("vm_PersonOrders")
//             .HasNoKey();
//
//         modelBuilder.Entity<Person>()
//             .HasMany(p => p.Orders)
//             .WithOne(o => o.Person)
//             .HasForeignKey(o => o.PersonId);
//     }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
//     }
// }

#endregion

#region Ef Core da view lerin özellikleri
// Key olmaz
// Changetracker ile takip edilmezler
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

        modelBuilder.Entity<PersonOrder>()
            .ToView("vm_PersonOrders")
            .HasNoKey();

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
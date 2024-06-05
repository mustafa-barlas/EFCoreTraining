//resharper disable all

using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new ApplicationDbContext();

#region Scalar fonksiyon

// Geriye değer döndüren fonksiyonlardır.

#region Scalar Function oluşturma

// 1. boş bir migration oluşturulmalı

// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@"CREATE FUNCTION  GETPERSONTOTALPRICE(@PERSONID INT)
//
//                         RETURNS INT
//                         AS 
//
//                         BEGIN
//                         DECLARE @TOTALPRICE INT
//
//                         SELECT  @TOTALPRICE = SUM(O.Price) FROM Persons P
//                         JOIN Orders O 
//                         ON P.PersonId = O.PersonId
//                         WHERE P.PersonId = 3
//                         RETURN @TOTALPRICE
//                         END");
// }
//
// /// <inheritdoc />
// protected override void Down(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@"DROP FUNCTION GETPERSONTOTALPRICE");
// }

#region HasDbFunction

// var persons = await (from person in context.Persons
//     where context.GETPERSONTOTALPRICEe(person.PersonId) > 5
//     select person).ToListAsync();
//
//
// class ApplicationDbContext1 : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//     public DbSet<Order> Orders { get; set; }
//     // public DbSet<PersonOrder> PersonOrders { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//
//         modelBuilder.Entity<Person>()
//             .HasMany(p => p.Orders)
//             .WithOne(o => o.Person)
//             .HasForeignKey(o => o.PersonId);
//
//
//         #region Scalar
//
//         modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext
//             .GETPERSONTOTALPRICEe), new[] { typeof(int) })).HasName("GETPERSONTOTALPRICEe");
//
//         #endregion
//     }
//
//     #region Scalar Function
//
//     public int GETPERSONTOTALPRICEe(int personId)
//     {
//         return personId;
//     }
//
//     #endregion
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
//     }
// }

#endregion

#endregion

#region Inline Functions

// geriye tablo döndüren fonksiyonlardır

//1. boş bir migrations oluşturulucak 


// public partial class v3 : Migration
// {
//     /// <inheritdoc />
//     protected override void Up(MigrationBuilder migrationBuilder)
//     {
//         migrationBuilder.Sql($@"CREATE FUNCTION BESTSELLING (@TOTALPRICE INT= 0)
//                     RETURNS TABLE
//                     AS
//                     RETURN 
//                     SELECT TOP 1 P.Name,COUNT(*) ORDERCOUNT, SUM(O.Price) TOTALPRICE FROM Persons P 
//                     JOIN Orders O 
//                     ON O.PersonId = P.PersonId
//                     GROUP BY P.Name
//                     HAVING SUM(O.Price) > @TOTALPRICE
//                     ORDER BY ORDERCOUNT DESC");
//     }
//
//     /// <inheritdoc />
//     protected override void Down(MigrationBuilder migrationBuilder)
//     {
//         migrationBuilder.Sql($@"DROP FUNCTION BESTSELLING");
//     }
// }


var personss = await context.BESTSELLING().ToListAsync();
Console.WriteLine();

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


[NotMapped]
public class BESTSELLING 
{
    public string Name { get; set; }
    public int OrderCount { get; set; }
    public int TotalPrice { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    // public DbSet<PersonOrder> PersonOrders { get; set; }

    #region Inline Scalar

    public IQueryable<BESTSELLING> BESTSELLING(int totalOrderPrice = 0) =>
        FromExpression(() => BESTSELLING(totalOrderPrice));

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext
            .BESTSELLING), new[] { typeof(int) })).HasName("BESTSELLING");

        modelBuilder.Entity<BESTSELLING>().HasNoKey();

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
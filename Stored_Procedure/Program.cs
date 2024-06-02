using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// resharper disable all
Console.WriteLine("Hello, World!");

ApplicationDbContext context = new ApplicationDbContext();

#region Store Procedure oluşturma

// 1. boş migration oluşturma
// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@"CREATE PROCEDURE SP_PERSONORDERS
// AS
//
// SELECT P.Name,COUNT(*) FROM Persons P 
// JOIN Orders O
// ON O.PersonId = P.PersonId
// GROUP BY P.Name
// ORDER BY COUNT(*) DESC");
// }
//
// /// <inheritdoc />
// protected override void Down(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@" drop SP_PERSONORDERS");
// }

#endregion

#region Store Procedure kullanma

#region FromSql

var datas = await context.PersonOrders.FromSql($"EXEC SP_PERSONORDERS").ToListAsync();

#endregion

#endregion

#region return değer döndüren Store Procedure  oluşturma

// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@"CREATE PROCEDURE SP_BEST_SELLING_STAFF
// AS
//
// DECLARE @NAME VARCHAR(50),@COUNT INT
//
// SELECT TOP 1 @NAME= P.Name, @COUNT= COUNT(*) FROM Persons P 
// JOIN Orders O
// ON O.PersonId = P.PersonId
// GROUP BY P.Name
// ORDER BY COUNT(*) DESC
// RETURN @COUNT");
// }
//
// /// <inheritdoc />
// protected override void Down(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@" drop SP_PERSONORDERS");
// }

SqlParameter countParameter = new()
{
    ParameterName = "count",
    SqlDbType = System.Data.SqlDbType.Int,
    Direction = ParameterDirection.Output
};
await context.Database.ExecuteSqlRawAsync($"EXEC @count = SP_BEST_SELLING_STAFF", countParameter);
Console.WriteLine(countParameter.Value);

#endregion

#region parametre alan Store Procedure  oluşturma

// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@"CREATE PROCEDURE SP_BEST_SELLING_STAFF
// (
//     @PersonId INT,
//     @NAME VARCHAR(50) OUTPUT
//     )
// AS
//
// SELECT @NAME = P.Name  FROM Persons P 
//     JOIN Orders O
// ON O.PersonId = P.PersonId
// WHERE P.PersonId = @PersonId");
// }
//
// /// <inheritdoc />
// protected override void Down(MigrationBuilder migrationBuilder)
// {
//     migrationBuilder.Sql($@" drop SP_PERSONORDERS");
// }


SqlParameter nameParameter = new()
{
    ParameterName = "name",
    SqlDbType = System.Data.SqlDbType.Int,
    Direction = ParameterDirection.Output,
    Size = 1000
};
await context.Database.ExecuteSqlRawAsync($"EXEC SP_BEST_SELLING_STAFF 5, @name Output", nameParameter);
Console.WriteLine(nameParameter.Value);

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

[NotMapped] // BU BİR TABLO DEĞİL 
public class PersonOrder // Kullanmak için 
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


        modelBuilder.Entity<PersonOrder>().HasNoKey();

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
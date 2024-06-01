// resharper disable all

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

ApplicationContext context = new();
Console.WriteLine();


#region FromSqlInterpolated

var persons = await context.Persons.FromSqlInterpolated($"""SELECT * FROM PERSONS WHERE Id = 5""").ToListAsync();

#endregion

#region FromSql

#region Query Execute

var persons1 = await context.Persons.FromSql($"""SELECT * FROM PERSONS""").ToListAsync();

#endregion

#region Stored Procedure Execute

var persons2 = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons null").ToListAsync();

#endregion

#region Parametreli  sorgu oluşturma

int id = 7;
SqlParameter personId = new("Id", "9"); // bunları zaten default olarak kendisi yapıyor
personId.DbType = DbType.Int32; // bunları zaten default olarak kendisi yapıyor
personId.Direction = ParameterDirection.Input; // bunları zaten default olarak kendisi yapıyor


var persons3 = await context.Persons.FromSql($"SELECT * FROM PERSONS WHERE ID={id}").ToListAsync();
var persons3_0_1 = await context.Persons.FromSql($"SELECT * FROM PERSONS WHERE ID={personId}").ToListAsync();
var persons3_1 = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {id}").ToListAsync();
var persons3_2 = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}").ToListAsync();
var persons3_3 = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons @PersonId ={personId}").ToListAsync();

// @personId store procedure deki parametre ne ise o yazılır

#endregion

#endregion

#region Dynamic SQL oluşturma ve parametre girme

// ef core cynamic şekilde oluşturulan sorgularda kolon isimleri parametreleştirilmişse o sorguyu execute etmez . Sql Injection zafiyetine dikkat edilmeli 
string personIdColumn = "PersonId";
string nameColumn = "Name";

SqlParameter personIdValue = new SqlParameter("PersonId", 87);
SqlParameter nameValue = new SqlParameter("Name", "John");

var persons15 = await context.Persons.FromSqlRaw(
        $"SELECT * FROM PERSONS WHERE {personIdColumn} = @PersonId AND {nameColumn} = @Name",
        personIdValue, nameValue)
    .ToListAsync();

#endregion

#region SqlQuery - NonEntity

// Entity'si olmayan scalar sorguların execute etmemizi sağlar.

var result = await context.Database.SqlQuery<int>($"SELECT PERSONID FROM PERSONS").ToListAsync();

var result0_1 = await context.Database.SqlQuery<int>($"SELECT PERSONID VALUE FROM PERSONS").Where(x => x > 5)
    .ToListAsync(); // ŞARTI VALUE OLARAK SUBQUERYE EKLİYOR

var result1 = await context.Persons.FromSql($"SELECT * FROM PERSONS").Where(x => x.Name.Contains("aslan"))
    .ToListAsync();

#endregion

#region ExecuteSql

// Insert ,Update, Delete

var ceyda = await context.Database.ExecuteSqlAsync($"UPDATE PERSONS SET NAME = 'CEYDA' WHERE PERSONID = 45");

#endregion

public class Person
{
    public int PersonId { get; set; }
    public string? Name { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}
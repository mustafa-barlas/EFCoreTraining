using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthwindEFCore;


#region ADONET

ConfigurationManager configuration = new();
configuration.GetConnectionString("appsettings.json");


await using SqlConnection connection = new("Server=MUSTAFABRLS ;Database=NorthwindDbContext;Trusted_Connection=true;TrustServerCertificate=True;");
await connection.OpenAsync();

SqlCommand command = new("SELECT * FROM EMPLOYES", connection);

SqlDataReader reader = await command.ExecuteReaderAsync();

while (await reader.ReadAsync())
{
    Console.WriteLine($"{reader["Firstname"]} {reader["Lastname"]}");
}

await connection.CloseAsync();

#endregion




#region ORM

NorthwindDbContext context = new();

var employeDatas = await context.Employees.ToListAsync();


#endregion
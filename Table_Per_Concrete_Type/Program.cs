// resharper disable all

using Microsoft.EntityFrameworkCore;

ApplicationContext context = new ApplicationContext();

#region TPC Configuration

// kalıtımsal ilişkiue sahip olan entitylerin oldugu çalışmalarda somut entitylere karşılık bir tablo oluşturucak bir modeldir. tpt nin performanslı versiyonudur.
// Base class Abstact olmak zoruda


#endregion

#region Veri eklme  güncelleme silme



#endregion


public abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
public class Employee : Person
{
    public string? Department { get; set; }

}
public class Customer : Person
{
    public string? CompanyName { get; set; }

}
public class Technician : Employee
{
    public string? Branch { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

using Microsoft.EntityFrameworkCore;
// resharper disable all


ApplicationContext context = new ApplicationContext();

#region TPH Configuration

// Hiyerarşiyi oluşturmak yeterlidir.

//public class Person
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//}
//public class Employee : Person
//{
//    public string? Department { get; set; }

//}
//public class Customer : Person
//{
//    public string? CompanyName { get; set; }

//}
//public class Technician : Employee
//{
//    public string? Branch { get; set; }

//}

//public class ApplicationContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Employee> Employees { get; set; }
//    public DbSet<Customer> Customers { get; set; }
//    public DbSet<Technician> Technicians { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
//    }
//}

#endregion

#region Discriminator Kolonu

//public class ApplicationContext2 : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Employee> Employees { get; set; }
//    public DbSet<Customer> Customers { get; set; }
//    public DbSet<Technician> Technicians { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Person>()
//            .HasDiscriminator<int>("Ayirici")
//            .HasValue<Person>(1)
//            .HasValue<Employee>(2)
//            .HasValue<Customer>(3)
//            .HasValue<Technician>(4);
//    }
//}

#endregion

#region Veri eklme

Employee employee = new Employee() { Id = 5, Name = "John", Surname = "Clark", Department = "IT" };
Technician technician = new Technician() { Id = 2, Name = "Brandon", Surname = "Parker", Department = "HR", Branch = "Bos" };
await context.Employees.AddAsync(employee);
await context.Technicians.AddAsync(technician);
await context.SaveChangesAsync();

#endregion

#region Veri güncellme



#endregion

#region Veri silme



#endregion
public class Person
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

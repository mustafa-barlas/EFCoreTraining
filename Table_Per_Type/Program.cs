using Microsoft.EntityFrameworkCore;
// resharper disable all

ApplicationContext context = new ApplicationContext();

#region TPT Configuration

// Her generate edilen tablolar hiyerarşik düzlemde kendi aralarında birebir ilişkiye sahiptir.



#endregion

#region Veri eklme  güncelleme silme

Technician technician = new Technician()
{
    Id = 1,
    Name = "Cevat",
    Surname = "Lark",
    Branch = "Master",
    Department = "IT"
};

Customer customer = new Customer() { Id = 4, Name = "Cevdet", Surname = "Olark", CompanyName = "Olark INC." };

Employee sil = await context.Employees.FindAsync(1);
context.Employees.Remove(sil);
await context.SaveChangesAsync();

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person>().ToTable("Person");
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Technician>().ToTable("Technician");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

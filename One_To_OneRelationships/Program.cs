using Microsoft.EntityFrameworkCore;

// ReSharper disable all
//Principal  kendi başına var olabilen entitydir.
//Dependent  kendi başına var olamayan entitydir.

class Program
{
    static void Main(string[] args)
    {

    }

}

#region Default Convention

//public class Employee
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public Address? Address { get; set; }

//}

//public class Address
//{
//    public int Id { get; set; }
//    public int? EmployeeId { get; set; }
//    public string? AddressText { get; set; }

//    public Employee? Employee { get; set; }
//}

#endregion

#region Data Annotations

//public class Employee
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public Address? Address { get; set; }

//}

//public class Address
//{
//    public int Id { get; set; }

//    [ForeignKey(nameof(Employee))]
//    public int? EmployeeId { get; set; }
//    public string? AddressText { get; set; }

//    public Employee? Employee { get; set; }
//}

// ***********************   OR
//public class Address
//{
//    [Key,ForeignKey(nameof(Employee))]
//    public int Id { get; set; }

//    public string? AddressText { get; set; }

//    public Employee? Employee { get; set; }
//}

#endregion

#region Fluent Api

//public class ETradeDbContext : DbContext
//{

//    public DbSet<Address> Addresses { get; set; }
//    public DbSet<Employee> Employees { get; set; }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>().HasKey(x => x.Id);

//        modelBuilder.Entity<Employee>()
//            .HasOne(x => x.Address)
//            .WithOne(x => x.Employee)
//            .HasForeignKey<Address>(x => x.Id);
//    }
//}
#endregion



public class ETradeDbContext : DbContext
{

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Employee> Employees { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Address? Address { get; set; }

}

public class Address
{
    public int Id { get; set; }

    public string? AddressText { get; set; }

    public Employee? Employee { get; set; }
}
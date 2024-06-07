using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

// Domain Driven Design yaklaşımında value objectlere karşılık olarak owned entity typeslar kullanılır.


#region OwnsOne Metodu

// class ApplicationDbContext1 : DbContext
// {
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Employee>().OwnsOne(x => x.EmployeeName, builder =>
//         {
//             builder.Property(x => x.MiddleName).HasColumnName("SecondName");
//         });
//         modelBuilder.Entity<Employee>().OwnsOne(x => x.Address);
//     }
// }

#endregion

#region OwnsMany Metodu

public class Employee1
{
    public int Id { get; set; }
    // public string Name { get; set; }
    // public string MiddleName { get; set; }
    // public string LastName { get; set; }
    // public string StreetAddress { get; set; }
    //public string Location { get; set; }
    public bool IsActive { get; set; }

    public EmployeeName? EmployeeName { get; set; }
    public Address? Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
}

public class Order
{
    public string? OrderDate { get; set; }
    public int Price { get; set; }
    
    public Employee? Employee { get; set; }
}

class ApplicationDbContext2 : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee1>()
            .OwnsMany(x => x.Orders, builder =>
            {
                builder.WithOwner().HasForeignKey("OwnedEmployeeId");
                builder.Property<int>("Id");
                builder.HasKey("Id");
            });
    }
}

#endregion




public class Employee
{
    public int Id { get; set; }

    // public string Name { get; set; }
    // public string MiddleName { get; set; }
    // public string LastName { get; set; }
    // public string StreetAddress { get; set; }
    //public string Location { get; set; }
    public bool IsActive { get; set; }

    public EmployeeName EmployeeName { get; set; }
    public Address Address { get; set; }
}

// [Owned]
public class EmployeeName
{
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
}


// [Owned]
public class Address
{
    public string StreetAddress { get; set; }
    public string Location { get; set; }
}


class ApplicationDbContext : DbContext
{
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //         "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
    //
    // }
}
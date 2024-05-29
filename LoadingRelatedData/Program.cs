using Microsoft.EntityFrameworkCore;
// resharper disable all

ApplicationContext context = new ApplicationContext();

#region Eager Loading Nedir?

// sorguya verilerin parçalı bir şekilde  eklenmesini sağlayan ve iradeli bir şekilde oluşturmamızı sağlar.

#region Include

var employees = await context.Employees.Include(x => x.Orders).ToListAsync();

#endregion

#region ThenInclude

var orders = await context.Orders   // İlişkili entity tekil ise bu yöntem kullanılabilir.
    .Include(x => x.Employee.Region)
    .ToListAsync();




var regions = await context.Regions // Collection bir yapı mevcut ise theninclude kullanılmak zorunda
    .Include(x => x.Employees)!
    .ThenInclude(x => x.Orders).ToListAsync();



#endregion

#region Filtered Include

var regionss = await context.Regions
    .Include(x => x.Employees.Where(x => x.Salary > 50000))
    .ToListAsync();

#endregion

#region AutoInclude


var employess = await context.Employees.ToListAsync();

public class ApplicationContext1 : DbContext
{

    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Employee>().Navigation(x => x.Orders).AutoInclude();
        modelBuilder.Entity<Employee>().Navigation(x => x.Region).AutoInclude();
    }
}
#endregion

#endregion












public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

}
public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public int RegionId { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public Region? Region { get; set; }

}
public class Region
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Employee>? Employees { get; set; }


}
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

}



public class ApplicationContext : DbContext
{

    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}
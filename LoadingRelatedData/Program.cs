using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

// resharper disable all
ApplicationContext context = new ApplicationContext();


#region Eager Loading Nedir?

// sorguya verilerin parçalı bir şekilde  eklenmesini sağlayan ve iradeli bir şekilde oluşturmamızı sağlar.

#region Include

var employees = await context.Employees.Include(x => x.Orders).ToListAsync();

#endregion

#region ThenInclude

var orders = await context.Orders // İlişkili entity tekil ise bu yöntem kullanılabilir.
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

// public class ApplicationContext1 : DbContext
// {
//
//     public DbSet<Person> Persons { get; set; }
//     public DbSet<Employee> Employees { get; set; }
//     public DbSet<Region> Regions { get; set; }
//     public DbSet<Order> Orders { get; set; }
//
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//
//         modelBuilder.Entity<Employee>().Navigation(x => x.Orders).AutoInclude();
//         modelBuilder.Entity<Employee>().Navigation(x => x.Region).AutoInclude();
//     }
// }

#endregion

#endregion

#region Explicit Loading

//  oluşturlan sorguya eklenecek verilerin sonradan gereksinimlerin ortaya çıkması ve belirlenmseinden sonra verilerin yüklenmesini  sağlayan yaklaşımdır.

var employee273 = await context.Employees.FirstOrDefaultAsync(x => x.Salary > 6485);
if (employee273.Name == "Mustafa")
{
    var orders25 = await context.Orders.Where(x => x.EmployeeId.Equals(employee273.Id)).ToListAsync();
}

var someResult = await context.Employees.Where(x => x.Salary > 646).ToListAsync();

if (someResult != null)
{
    foreach (var some in someResult)
    {
        await context.Entry(some).Collection(x => x.Orders).LoadAsync();
    }
}


#region Reference // Tekil

var employee27 = await context.Employees.FirstOrDefaultAsync(x => x.Id == 2);

await context.Entry(employee27).Reference(x => x.Region).LoadAsync();

#endregion

#region Collection // Çogul

await context.Entry(employee27).Collection(x => x.Orders).LoadAsync();

#endregion

#region Collection lar da Aggregate fonksiyonları uygulamak

//
// var employee7 = await context.Employees.FirstOrDefaultAsync(x => x.Id == 5);
//
// var count = await context.Entry(employee7).Collection(x => x.Orders).Query().CountAsync();

#endregion

#region Collection lar da Filtreleme aksiyonunu uygulamak

var employee7 = await context.Employees.FirstOrDefaultAsync(x => x.Id == 5);

var date = await context.Entry(employee7).Collection(x => x.Orders).Query()
    .Where(x => x.OrderDate.Day == DateTime.Now.Day).ToListAsync();

#endregion

#endregion

#region Lazy Loading

#region Proxy ile lazy loading

// eğer proxy üzerinden lazy loading kullanılıcak ise navigation proprty ler virtual olmalı

// public class Person1
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
// }
//
// public class Employee1
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
//     public int Salary { get; set; }
//
//     public int RegionId { get; set; }
//     public virtual ICollection<Order>? Orders { get; set; }
//     public virtual Region? Region { get; set; }
// }
//
// public class Region1
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//
//     public virtual ICollection<Employee>? Employees { get; set; }
// }
//
// public class Order1
// {
//     public int Id { get; set; }
//     public DateTime OrderDate { get; set; }
//     public int EmployeeId { get; set; }
//     public virtual Employee? Employee { get; set; }
// }
//
// public class ApplicationContext1 : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//     public DbSet<Employee> Employees { get; set; }
//     public DbSet<Region> Regions { get; set; }
//     public DbSet<Order> Orders { get; set; }
//
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
//             "Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
//         // Veya 
//         optionsBuilder.UseLazyLoadingProxies();
//     }
// }

#endregion

#region Proxy olmadan lazy loading

#region ILazyLoader interface

// public class Employee2
// {
//     private ILazyLoader _lazyLoader;
//     private Region2 _region2;
//
//     public Employee2()
//     {
//     }
//
//     public Employee2(ILazyLoader lazyLoader)
//     {
//         _lazyLoader = lazyLoader;
//     }
//
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
//     public int Salary { get; set; }
//
//     public int RegionId { get; set; }
//     public ICollection<Order>? Orders { get; set; }
//
//     public Region2? Region
//     {
//         get => _lazyLoader.Load(this, ref _region2);
//         set => _region2 = value;
//     }
// }
//
// public class Region2
// {
//     private ILazyLoader _lazyLoader;
//     private Employee2 _employee2;
//
//     public Region2()
//     {
//     }
//
//     public Region2(ILazyLoader lazyLoader)
//     {
//         _lazyLoader = lazyLoader;
//     }
//
//
//     public int Id { get; set; }
//     public string? Name { get; set; }
//
//     public Employee2? Employees
//     {
//         get => _lazyLoader.Load(this, ref _employee2);
//         set => _employee2 = value;
//     }
// }

#endregion

#region Delegate ile lazy loading

// static class LazyLoadingExtension
// {
//     public static TRelated Load<TRelated>(this Action<object, string> loader, object entity, ref TRelated navigation,
//         [CallerMemberName] string navigationName = null)
//     {
//         loader.Invoke(entity, navigationName);
//         return navigation;
//     }
// }
//
// public class Employee3
// {
//     private Action<object, string> _lazyLoader;
//     private Region3 _region3;
//
//     public Employee3()
//     {
//     }
//
//     public Employee3(Action<object, string> lazyLoader)
//     {
//         _lazyLoader = lazyLoader;
//     }
//
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
//     public int Salary { get; set; }
//     public int RegionId { get; set; }
//
//     public Region3? Region
//     {
//         get => _lazyLoader.Load(this, ref _region3);
//         set => _region3 = value;
//     }
// }
//
// public class Region3
// {
//     private Action<object, string> _lazyLoader;
//     private ICollection<Employee3>? _employee3;
//
//     public Region3()
//     {
//     }
//
//     public Region3(Action<object, string> lazyLoader)
//     {
//         _lazyLoader = lazyLoader;
//     }
//
//     public int Id { get; set; }
//     public string? Name { get; set; }
//
//     public ICollection<Employee3>? Employees
//     {
//         get => _lazyLoader.Load(this, ref _employee3);
//         set => _employee3 = value;
//     }
// }

#endregion

#endregion

#region N+1 Problemi

//  sorguladıgımız kolonlar  döngüsel işlemlerde her defasında tekrar sorgu oluşturulup ve sorgu gönderme işlemi yapar. maliyetli bir durumdur.

var region55 = await context.Regions.FindAsync(56);
foreach (var employee in region55.Employees)
{
    var orderss = employee.Orders;
    foreach (var order in orderss)
    {
        Console.WriteLine(order.OrderDate);
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
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}
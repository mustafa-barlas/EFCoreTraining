// resharper disable all

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
ApplicationContext1 context1 = new ApplicationContext1();

#region Temporal Tables nedir

// Veri değişikliği süreçlerinde kayıtları depolayan  ve sistem tarafından yönetilen tablolardır.

#endregion

#region Temporal Tablesın Uygulanması

#region IsTemporal yapılanması

// public class Employee1
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
// }
//
// public class ApplicationContext1 : DbContext
// {
//     public DbSet<Employee1> Employees1 { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Employee1>().ToTable("Employee1", builder => builder.IsTemporal());
//     }
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = TemporalExample; Trusted_Connection = true; TrustServerCertificate = True;");
//     }
// }

#endregion

// veri ekleme süreçlerinde herhangi bir kayıt atılmaz. var olan veriler üzerindeki değişimleri takip eder.

#region Veri Ekleme

var employees = new List<Employee1>()
{
    new Employee1() { Name = "Ceyda", Surname = "Kalkancı" },
    new Employee1() { Name = "Murat", Surname = "Kara" },
    new Employee1() { Name = "Hakan", Surname = "Kara" },
    new Employee1() { Name = "Nisa", Surname = "Kara" },
    new Employee1() { Name = "Sema", Surname = "Kara" },
    new Employee1() { Name = "Ebru", Surname = "Kara" },
    new Employee1() { Name = "Okan", Surname = "Kara" },
    new Employee1() { Name = "Yılmaz", Surname = "Kara" },
};

// await context1.Employees1.AddRangeAsync(employees);
// context1.SaveChanges();

#endregion

#region Veri Güncelleme

var employee = await context1.Employees1.FindAsync(5);
// employee.Name = "Gül";
// employee.Surname = "Coşkun";
// await context1.SaveChangesAsync();

#endregion

#region Veri Silme

var employee5 = await context1.Employees1.FindAsync(5);
// context1.Employees1.Remove(employee5);
// await context1.SaveChangesAsync();

#endregion

#endregion

#region TemporalAsOf

// Belirli bir zaman içinde değişikliğe uğrayan tüm öğeleri döndüren bir fonksiyondur.

var result = await context1.Employees1.TemporalAsOf(new DateTime(2024, 06, 08, 10, 57, 00)).Select(x => new
{
    x.Name,
    x.Surname,
    PeriodStart = EF.Property<DateTime>(x, "PeriodStart"),
    PeriodEnd = EF.Property<DateTime>(x, "PeriodEnd"),
}).ToListAsync();

// foreach (var item in result)
// {
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(item.Name + "\n" + item.Surname +"\n"+ item.PeriodStart + "\n" + item.PeriodStart);
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(result.Count);
// }

#endregion

#region Temporal All

// Güncellenmiş veya sililnmiş tüm verileri gertirir.
var result1 = await context1.Employees1.TemporalAll().Select(x => new
{
    x.Name,
    x.Surname,
    PeriodStart = EF.Property<DateTime>(x, "PeriodStart"),
    PeriodEnd = EF.Property<DateTime>(x, "PeriodEnd"),
}).ToListAsync();

// foreach (var item in result1)
// {
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(item.Name + "\n" + item.Surname +"\n"+ item.PeriodStart + "\n" + item.PeriodStart);
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(result.Count);
// }

#endregion

#region TemporalFromTo

// belirli zaman aralığındaki verileri döndürür.

var baslancic = new DateTime(2024, 06, 08, 10, 56, 45);
var bitis = new DateTime(2024, 06, 08, 10, 58, 45);
var result2 = await context1.Employees1.TemporalFromTo(baslancic, bitis).Select(x => new
{
    x.Name,
    x.Surname,
    PeriodStart = EF.Property<DateTime>(x, "PeriodStart"),
    PeriodEnd = EF.Property<DateTime>(x, "PeriodEnd"),
}).ToListAsync();

// foreach (var item in result2)
// {
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(item.Name + "\n" + item.Surname + "\n" + item.PeriodStart + "\n" + item.PeriodStart);
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(result.Count);
// }

#endregion

#region TemporalBetween

// belirli zaman aralığındaki verileri döndürür.başlangıç verisi dahil değil. bitiş verisi dahil

var baslancic3 = new DateTime(2024, 06, 08, 10, 56, 45);
var bitis3 = new DateTime(2024, 06, 08, 10, 58, 45);
var result3 = await context1.Employees1.TemporalBetween(baslancic3, bitis3).Select(x => new
{
    x.Name,
    x.Surname,
    PeriodStart = EF.Property<DateTime>(x, "PeriodStart"),
    PeriodEnd = EF.Property<DateTime>(x, "PeriodEnd"),
}).ToListAsync();

// foreach (var item in result2)
// {
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(item.Name + "\n" + item.Surname + "\n" + item.PeriodStart + "\n" + item.PeriodStart);
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(result.Count);
// }

#endregion

#region TemporalContainIn

// belirli zaman aralığındaki verileri döndürür.başlangıç ve. bitiş verisi dahildir.

var baslancic4 = new DateTime(2024, 06, 08, 10, 56, 45);
var bitis4 = new DateTime(2024, 06, 08, 10, 58, 45);
var result4 = await context1.Employees1.TemporalBetween(baslancic3, bitis3).Select(x => new
{
    x.Name,
    x.Surname,
    PeriodStart = EF.Property<DateTime>(x, "PeriodStart"),
    PeriodEnd = EF.Property<DateTime>(x, "PeriodEnd"),
}).ToListAsync();

// foreach (var item in result4)
// {
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(item.Name + "\n" + item.Surname + "\n" + item.PeriodStart + "\n" + item.PeriodStart);
//     Console.WriteLine("--------------------------------------");
//     Console.WriteLine(result4.Count);
// }

#endregion

#region Silinmiş veri  nasıl getirilir

// silinmiş verinin tarihi bilinmeli.
var _date = await context1.Employees1
    .TemporalAll()
    .Where(x => x.Id == 16)
    .OrderByDescending(x => EF.Property<DateTime>(x, "PeriodEnd"))
    .Select(x => EF.Property<DateTime>(x, "PeriodEnd"))
    .FirstAsync();

var fatma = await context1.Employees1.TemporalAsOf(_date.AddMilliseconds(-1)).FirstOrDefaultAsync(x => x.Id.Equals(16));

await context1.AddAsync(fatma);
await context1.Database.OpenConnectionAsync();
await context1.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee1 ON");
await context1.SaveChangesAsync();
// await context1.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee1 OF");
#endregion

public class Employee1
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class ApplicationContext1 : DbContext
{
    public DbSet<Employee1> Employees1 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee1>().ToTable("Employee1", builder => builder.IsTemporal());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = TemporalExample; Trusted_Connection = true; TrustServerCertificate = True;");
    }
}


// public class Person
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
//     public DateTime DateOfBirth { get; set; }
// }
//
// public class Employee
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Surname { get; set; }
// }
//
// public class ApplicationContext : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//     public DbSet<Employee> Employees { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Employee>().ToTable("Employee", builder => builder.IsTemporal());
//     }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server = MUSTAFABRLS; Database = ApplicationDb; Trusted_Connection = true; TrustServerCertificate = True;");
//     }
// }
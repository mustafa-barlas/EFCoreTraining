using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//resharper disable  all
Console.WriteLine("Hello, World!");


#region HasConversion

// public class ApplicationContext : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Person>().Property(x => x.Gender)
//             .HasConversion(
//                 g => g.ToUpper(), // insert update
//                 g => g == "M" ? "Male" : "Female"); // select
//         base.OnModelCreating(modelBuilder);
//     }
// }

#endregion

#region Enum da Convertors

// Default olarak int eklenir veritabanına


// public class ApplicationContext : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         
//         modelBuilder.Entity<Person>()
//             .Property(p => p.Gender2)
//             .HasConversion(
//                 v => v.ToString(), // Enum'dan string'e dönüşüm
//                 v => (Gender)Enum.Parse(typeof(Gender), v) // String'den enum'a dönüşüm
//             );
//     }
// }

#endregion

#region Value Convertor Class

// public class ApplicationContext : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         ValueConverter<Gender, string> converter = new(
//             g => g.ToString(),
//             g => (Gender)Enum.Parse(typeof(Gender), g)
//         );
//
//         modelBuilder.Entity<Person>().Property(x => x.Gender2).HasConversion(converter);
//     }
// }

#endregion

#region CustomValueConvertor

// public class GenderConvertor : ValueConverter<Gender, string>
// {
//     public GenderConvertor() : base(g => g.ToString(), g => (Gender)Enum.Parse(typeof(Gender), g))
//     {
//     }
// }
//
//
// public class ApplicationContext : DbContext
// {
//     public DbSet<Person> Persons { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Person>().Property(x => x.Gender2).HasConversion<GenderConvertor>();
//     }
// }

#endregion

#region İlkel koleksiyonların serilizasyonu

public class ApplicationContext1 : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().Property(l => l.List).HasConversion(
            l => JsonSerializer.Serialize(l, (JsonSerializerOptions)null),
            l => JsonSerializer.Deserialize<List<string>>(l, (JsonSerializerOptions)null)
        );
    }
}

#endregion


public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public bool Married { get; set; } = false;
    public Enum? Gender2 { get; set; }

    public List<string>? List { get; set; } = new List<string>();
}

public enum Gender
{
    Male,
    Female
}

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BoolToStringConverter stringConverter = new("bkear", "evli");

        BoolToTwoValuesConverter<char> charTwoValuesConverter = new('K', 'E');

        modelBuilder.Entity<Person>().Property(x => x.Married).HasConversion<BoolToZeroOneConverter<int>>();
        modelBuilder.Entity<Person>().Property(x => x.Married).HasConversion<string>();

        modelBuilder.Entity<Person>().Property(x => x.Married).HasConversion(stringConverter);
        modelBuilder.Entity<Person>().Property(x => x.Married).HasConversion(charTwoValuesConverter);
    }
}
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;

//resharper disable all


Console.WriteLine();


#region Sequence tanımlama

// benzersiz ve ardışık sayısal değerler üreten veritabanı nesnesidir. tablo özeliği deildir. birden fazla tablo tarafından kullanılabilir.

public class ApplicationContext1 : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // 
    {
    }
}

#endregion


public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}


public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) // 
    {
    }
}
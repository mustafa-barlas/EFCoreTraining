using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//resharper disable all

Console.WriteLine();

#region IEntityTypeConfiguration<T> Arayüzü

class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(14);
    }
}

public class ApplicationContext2 : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());

        // Veya

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

#endregion


public class Person
{

    public int Id { get; set; }

    public int PersonCode { get; set; }

    public string? Name { get; set; }

    public Address? Address { get; set; }
}
public class Address
{
    public int Id { get; set; }
    public string? Text { get; set; }

    public Person? Person { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
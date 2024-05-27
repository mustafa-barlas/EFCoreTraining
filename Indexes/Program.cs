using Microsoft.EntityFrameworkCore;
ApplicationContext context = new ApplicationContext();
//resharper disable all


#region Index Attribute

[Index(nameof(Name))]
public class Person1
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}



#endregion

#region Index Fluent Api

public class ApplicationContext1 : DbContext
{
    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person1>().HasIndex(x => x.Name);
    }

}

#endregion

#region Composite key

[Index(nameof(Name), nameof(Surname))]
public class Person2
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}



public class ApplicationContext2 : DbContext
{
    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person1>().HasIndex(x => new { x.Name, x.Surname });
        modelBuilder.Entity<Person1>().HasIndex(nameof(Person.Name), nameof(Person.Surname));
    }

}

#endregion

#region Index Uniqueness

[Index(nameof(Name), IsUnique = true)]
public class Person3
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}


public class ApplicationContext3 : DbContext
{
    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person3>().HasIndex(nameof(Person.Name)).IsUnique();
    }

}

#endregion

#region Index Sort Order

[Index(nameof(Name), nameof(Surname), AllDescending = true)]                  // tüm indexlerde uygulanır.
[Index(nameof(Name), nameof(Surname), IsDescending = new[] { true, false })] //  ilki sıralanacak ikincisi sıralanmaycak.
public class Person4
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}


public class ApplicationContext4 : DbContext
{
    public DbSet<Person> Persons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {
        modelBuilder.Entity<Person>()
            .HasIndex(x => new { x.Name, x.Surname })
            .IsDescending(true, false); //  ilki sıralanacak ikincisi sıralanmaycak.


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


    protected override void OnModelCreating(ModelBuilder modelBuilder)  // 
    {

    }

}

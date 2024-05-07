// ReSharper disable all

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



#region Default Convention

//public class Employee
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public int? DepartmentId { get; set; } // Zorunlu değil
//    public Department? Department { get; set; }
//}

//public class Department
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Department>? Departments { get; set; }
//}

#endregion

#region Data Annotations

//public class Employee
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    [ForeignKey(nameof(Department))]
//    public int? DepartmentId { get; set; } // Zorunlu 
//    public Department? Department { get; set; }
//}

//public class Department
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Department>? Departments { get; set; }
//}

#endregion

#region Fluent Api

//public class Employee
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public int? DepartmentId { get; set; }  // Zorunlu değil
//    public Department? Department { get; set; }
//}

//public class Department
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public ICollection<Employee>? Employees { get; set; }
//}


//public class ETradeDbContext : DbContext
//{

//    public DbSet<Department> Departments { get; set; }
//    public DbSet<Employee> Employees { get; set; }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;");
//    }


//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Employee>()
//            .HasOne(x => x.Department)
//            .WithMany(x => x.Employees)
//            .HasForeignKey(x => x.DepartmentId); // Zorunlu değil
//    }
//}

#endregion



public class ETradeDbContext : DbContext
{

    public DbSet<Department> Departments { get; set; }
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
}

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }

}
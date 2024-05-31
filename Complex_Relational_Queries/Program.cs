using Microsoft.EntityFrameworkCore;

// resharper disable all
ApplicationContext context = new ApplicationContext();


#region Complex Query Operators

#region Join

#region Query Syntax

var query = from photo in context.Photos
    join person in context.Persons
        on photo.PersonId equals person.PersonId
    select new
    {
        person.Name,
        photo.Url
    };

#endregion

#region Method Syntax

var query2 = context.Photos.Join(context.Persons, photo => photo.PersonId, person => person.PersonId,
    (photo, person)
        => new
        {
            person.Name,
            photo.Url
        });

#endregion

#region Multiple columns join

var query3 = from photo in context.Photos
    join person in context.Persons
        on new { photo.PersonId, photo.Url } equals new { person.PersonId, Url = person.Name }
    select new
    {
        person.Name,
        photo.Url
    };


var query4 = context.Persons.Join(
    context.Photos,
    person => new { person.PersonId, Url = person.Name },
    photo => new { photo.PersonId, photo.Url },
    (person, photo) => new
    {
        person.Name,
        photo.Url
    });

#endregion

#region 2'den fazla tablo ile join işlemi

var query5 = from photo in context.Photos
    join person in context.Persons
        on photo.PersonId equals person.PersonId
    join order in context.Orders
        on photo.PersonId equals order.PersonId
    select new
    {
        person.Name,
        photo.Url,
        order.Description
    };


var query6 = context.Photos.Join(
        context.Persons,
        photo => photo.PersonId,
        person => person.PersonId,
        (photo, person) => new
        {
            person.PersonId,
            person.Name,
            photo.Url
        })
    .Join(context.Orders,
        oncekiSorgu => oncekiSorgu.PersonId,
        order => order.PersonId,
        (oncekiSorgu, order) => new
        {
            oncekiSorgu.PersonId,
            oncekiSorgu.Name,
            oncekiSorgu.Url,
            order.OrderId,
            order.Description
        });

#endregion

#region Group Join

var query7 = from order in context.Orders
    join person in context.Persons
        on order.PersonId equals person.PersonId into personOrders //  left ve right join imkanı sağlar
    //from person  in personOrders
    select new
    {
        Count = personOrders.Count(),
        personOrders,
        Name = personOrders.Select(x => x.Name),
        Id = personOrders.Select(x => x.PersonId),
        order.Description,
        // person.Name
    };

#endregion

#region Left Join

// sağ tarafta bişey varsa getir yoksa null la

var query8 = (from person in context.Persons
    join order in context.Orders
        on person.PersonId equals order.PersonId into personOrders
    from order in personOrders.DefaultIfEmpty()
    select new
    {
        person.Name,
        order.Description
    });

#endregion

#region Right Join

// sol tarafta bişey varsa getir yoksa null la. ef core da right join yoktur. left joini al tabloların yerini değiştir.

var query9 = from order in context.Orders
    join person in context.Persons
        on order.PersonId equals person.PersonId into orderPersons
    from person in orderPersons.DefaultIfEmpty()
    select new
    {
        person.Name,
        person.PersonId,
        order.Description,
        order.OrderId
    };

#endregion

#region Full Join

// veri  varsa getir yoksa null la. ef core da full join yoktur. önce left sonra right join yap.

var left = from person in context.Persons
    join order in context.Orders
        on person.PersonId equals order.PersonId into personOrders
    from order in personOrders.DefaultIfEmpty()
    select new
    {
        person.Name,
        person.PersonId,
        order.Description,
        order.OrderId
    };

var right = from order in context.Orders
    join person in context.Persons
        on order.PersonId equals person.PersonId into orderPersons
    from person in orderPersons.DefaultIfEmpty()
    select new
    {
        person.Name,
        person.PersonId,
        order.Description,
        order.OrderId
    };

var fullJoin = left.Union(right);

#endregion

#region Cross Join

var query11 = from order in context.Orders
    from person in context.Persons
    select new
    {
        order,
        person
    };

#endregion

#region Collection Selector da where kullanımı // Inner join olarak algılar

var query12 =
    from order in context.Orders
    from person in context.Persons.Where(x => x.PersonId == order.PersonId)
    select new
    {
        order,
        person
    };

#endregion

#region Cross Apply

// Inner join

var query13 = from person in context.Persons
    from order in context.Orders.Select(x => person.Name)
    select new
    {
        order,
        person
    };

#endregion

#region Outer Apply

// left join

var query14 = from person in context.Persons
    from order in context.Orders.Select(x => x.Person.Name).DefaultIfEmpty()
    select new
    {
        order,
        person
    };

#endregion

#endregion

#endregion
  

Console.WriteLine();

public class Person
{
    public int PersonId { get; set; }
    public string? Name { get; set; }
    public Gender? Gender { get; set; }
    public Photo? Photo { get; set; }
    public ICollection<Order>? Orders { get; set; }
}

public class Gender
{
}

public class Photo
{
    public int PersonId { get; set; }
    public string? Url { get; set; }
    public Person? Person { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public string? Description { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}


public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = MUSTAFABRLS; Database = Library; Trusted_Connection = true; TrustServerCertificate = True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().HasKey(x => x.PersonId);

        modelBuilder.Entity<Photo>()
            .HasOne(x => x.Person)
            .WithOne(x => x.Photo);


        modelBuilder.Entity<Person>()
            .HasMany(x => x.Orders)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId);
    }
}
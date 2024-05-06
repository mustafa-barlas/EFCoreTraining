using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
// ReSharper disable all
EticaretContextDb context = new EticaretContextDb();

#region Change Tracking nedir

// Context üzerinden gelen tüm veriler oto şekilde takip mekanizması tarafından izlenir buna changetracker denir.

#endregion

#region ChangeTracker Propertysi

// takip edilen nesnelere erişebilmemizi ve gerektiinde işlemler yapmamaızı sağlar.

var urunler = await context.Uruns.ToListAsync();

var urun = urunler[7].Fiyat = 45;

var datas = context.ChangeTracker.Entries();

#endregion

#region DetectChanges Metodu
// emin olmak için kullanılabilir. opsiyonel olarak changetracker sistemini çalıştırmamızı sağlar.

var urunr = await context.Uruns.FirstOrDefaultAsync(x => x.Id == 5);
urunr.Fiyat = 51;

context.ChangeTracker.DetectChanges();
await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Metodu

// detectchanges konfigürsayonu kenidmiz yapacaksak

var urunn = await context.Uruns.FirstOrDefaultAsync(x => x.Id == 5);
urunn.Fiyat = 51;


context.ChangeTracker.DetectChanges();
await context.SaveChangesAsync();

#endregion

#region Entries Metodu

// Context teki entry metodunun  koleksiyonel versiyonudur. changetracker izlenen verilerin bilgisini entityentry türünde elde etmemizi  sağlar.
// entries detectchanges metodunu tetikler. maliyetlidir.

var products = context.Uruns.ToList();
products[12].Fiyat = 44;
products.FirstOrDefault(x => x.Id == 45);
context.Uruns.Remove(products.SingleOrDefault(x => x.Id.Equals(20)));

context.ChangeTracker.Entries().ToList().ForEach(e =>
{
    if (e.State == EntityState.Added)
    {

    }
    else if (e.State == EntityState.Modified)
    {

    }
});


#endregion

#region AcceptAllChanges Metodu

// takip edilen tüm nesnelerin takibini keser. bu yüzden savechanges(false) ve acceptAllchanges var
// acceptallchanges iradeli şekilde takibi bırakır.
// savechanges(false) true veya false durumunda biz acceptallchanges metodunu çağırana kadar verileri takip eder.


var productss = context.Uruns.ToList();
products[12].Fiyat = 44;
products.FirstOrDefault(x => x.Id == 45);
context.Uruns.Remove(products.SingleOrDefault(x => x.Id.Equals(20)));

context.SaveChanges(true);
context.ChangeTracker.AcceptAllChanges();


#endregion

#region HasChanges Metodu

// takip edilen nesneler arasında değişiklik varmı yokmu bunun bilgisini bize verir.

var productsss = context.Uruns.ToList();
products[12].Fiyat = 44;
products.FirstOrDefault(x => x.Id == 45);
context.Uruns.Remove(products.SingleOrDefault(x => x.Id.Equals(20)));

#endregion

#region Entity States

// Entity nesnelerinin durumlarını ifade eder.

Urun uruna = new Urun();
Console.WriteLine(context.Entry(uruna).State);

// Detached    
// Added
// Modified
// Deleted
// Unchanged

#endregion

#region ChangeTracker ın Interceptor olarak kullanılması



#endregion

#region Context üzerinden changetracker

var urunlerr = context.Uruns.FirstOrDefault(x => x.Id.Equals(55));

context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urun.Fiyat));

context.Entry(urun).CurrentValues.GetValue<decimal>(nameof(Urun.Fiyat));
context.Entry(urun).GetDatabaseValues();

#endregion

public class EticaretContextDb : DbContext
{

    public DbSet<Urun> Uruns { get; set; }
    public DbSet<Parca> Parcas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Test1; Trusted_Connection = true; TrustServerCertificate = True;");
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcas { get; set; }
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }
}
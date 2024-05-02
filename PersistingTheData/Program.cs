using Microsoft.EntityFrameworkCore;
// ReSharper disable All


EticaretContextDb context = new EticaretContextDb();



#region Veri Ekleme

Urun urun12 = new Urun()
{
    Name = "Ürün 1",

};
Urun urun22 = new Urun()
{
    Name = "Ürün 2",

};

//var state = context.Entry(urun12).State;  // Entity durumunu görüntüleme 
//Console.WriteLine(state);


//await context.AddAsync(urun12);       //
//await context.Uruns.AddAsync(urun12); //   tip güvenli
//await context.Uruns.AddRangeAsync(urun12, urun22);
//await context.SaveChangesAsync(); //  işlemler başarısız olur ise işlemleri geri alır


#endregion



#region Veri Günceleme



Urun? urun4 = await context.Uruns.FirstOrDefaultAsync(x => x.Id == 1);
urun4.Name = "urun abc";

//await context.SaveChangesAsync();


#endregion

#region ChangeTracker Nedir

// ChangeTracker  context üzerinden gelen verilerin takibinden sorumludur. Bu Mekanizma sayesinde  takip bilgisi hakkında bilgilere ulaşabiliriz. Bu özellik, bir nesnenin bir veritabanındaki durumu ile nesnenin bellekteki durumu arasındaki farkları takip etmeye yarar.

#endregion

#region Takip Edilmeyen Nesneler Nasıl Güncellenir

Urun urun2 = new()
{
    Id = 1,
    Name = "urun5"
};

//güncelleme için id verilmesi gerek

#region Update Fonksiyonu

//context.Uruns.Update(urun2);
//await context.SaveChangesAsync();

#endregion

#endregion

#region EntityState Nedir

//Bir entity instance ın durumunu ifade eden bir referanstır.

Urun? urun1 = await context.Uruns.FirstOrDefaultAsync(x => x.Id.Equals(1));
urun1.Name = "dsfdghjhkj";

//Console.WriteLine(context.Entry(urun1).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun1).State);


#endregion

#region EFCore da Verinin güncelenmesi gerektiğini nasıl anlarız



#endregion

#region Birden Fazla Veri Güncellenirken

var urunler = await context.Uruns.ToListAsync();

int i = 0;
foreach (var urun in urunler)
{
    urun.Name = "olala";

}

//await context.SaveChangesAsync();

#endregion


#region Veri Silme işlemi

Urun? urunA = await context.Uruns.FirstOrDefaultAsync(x => x.Id == 2);
//context.Uruns.Remove(urunA);
//await context.SaveChangesAsync();

#endregion

#region Takip edilmeyen nesneler nasıl silinir

Urun u = new()
{
    Id = 10
};

context.Uruns.Remove(u);
context.SaveChanges();


#endregion

#region Birden fazla veri silinirken

foreach (var urun in context.Uruns)
{
    context.Uruns.RemoveRange(urun);
}

#endregion

#region EntityState ile veri silinirken

Urun ur = new()
{
    Id = 10
};

context.Entry(ur).State = EntityState.Deleted;
context.SaveChanges();

#endregion

public class EticaretContextDb : DbContext
{

    public DbSet<Urun> Uruns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = Test1; Trusted_Connection = true; TrustServerCertificate = True;");
    }

}



public class Urun
{
    public int Id { get; set; }
    public string Name { get; set; }
}
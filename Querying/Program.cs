using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

//ReSharper disable all
EticaretContextDb context = new EticaretContextDb();

#region Temel Sorgulama

#region Method Syntax

var urunler = await context.Uruns.ToListAsync();

#endregion

#region QuerySyntax

var urunler2 = await (from urun in context.Uruns
                      select urun).ToListAsync();

#endregion

#endregion

#region Execute etmek için ne yapmalıyız

//ToList();
//foreach();

#endregion

#region IQueryable ve IEnumerable nedir

//IQueryable sorguya karşılık gelir execute edilmemiş halidir.
//IEnumerable sorguya karşılık gelir execute edilmiş halidir. in memorye yüklenmiş halidir.

var urunler3 = from urun in context.Uruns select urun;                    // IQueryable
var urunler4 = await (from urun in context.Uruns select urun).ToListAsync(); // IEnumerable

#endregion

#region Çoğul veri getirme sorguları

//ToList();
//Where
//OrderBy
//ThenBy order by üzerinden yapılan sıralama işlemine ekstra kolon eklemek için kllanılır.

#endregion

#region Tekil veri getirme sorguları

//Single()             //   birden fazla geliyorsa veya hiç veri gelmiyorsa hata fırlatır
//SingleOrDefault()   //    birden fazla veri  geliyor ise hata fırlatır gelmiyor is null döner
//First()            //     yapılan sorguda  bir tane veri elde etmek istiyorsan . ilkini getirir. veri yoksa hata fırlatır
//FirstOrDefault()  //      yapılan sorguda  bir tane veri elde etmek istiyorsan . ilkini  veya default olanı getirir. veri yoksa null döner
//Last()           //       yapılan sorguda sonuncu veriyi elde etmek istiyorsan. yoksa hata fırlatır
//LastOrDefault() //        yapılan sorguda sonuncu veriyi elde etmek istiyorsan.  sonuncusunu  veya default olanı getirir. veri yoksa null döner
//Find()         //         primary key  kolonuna özel  sorgulama yapmamızı sağlayan fonksiyondur. composit şekilde de arama yapabiliriz. await context.UrunParca.FindAsync(2,8); yoksa null döner
#endregion

#region Diğer sorgulama yöntemleri

//Count()
//LongCount()
//Any() // sql de exist komutunun karşılıgır.  true veya false döndürür.
//Max()
//Min()
//Distinct()  //  mükerrer kayıtları tekileştirecek
//All()   // sorgu neticesinde gelen tüm veriler şarta uyar ise true dönecektir. ama hepsi şarta uymalı
//Average()
//Sum()
//Contains()
//StartsWith()
//EndsWith()

#endregion

#region Sorgu sonucu dönüşüm yöntemleri

#region ToDictionaryAsync

var uruns = await context.Uruns.ToDictionaryAsync(x => x.UurnAdi, x => x.Fiyat);


#endregion

#region ToArrayAsync

// sorguyu entity dizi olarak elde eder

var uruns2 = await context.Uruns.ToArrayAsync();

#endregion

#region Select

// sorgunun çekilecek kolonlarını ayarlamamızı sağlar
// select fonskiyonu farklı türde ayarlayabilme imkanı sağlar. anonim,T,Entity

var uruns3 = context.Uruns.Select(x => new
{
    Id = x.Id,
    Adı = x.UurnAdi,
}).ToListAsync();



#endregion

#region SelectMany

// ilişkisel tablolar sonucunda  gelen koleksiyonel verileri projeksiyon etmemizi sağlar

var uruns4 = context.Uruns
    .Include(x => x.Parcas)
    .SelectMany(x => x.Parcas, (x, p) => new
    {
        x.Id,
        x.Fiyat,
        p.ParcaAdi
    });

#endregion

#endregion

#region GroupBy



#endregion

#region Foreach



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
    public string UurnAdi { get; set; }
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
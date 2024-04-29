using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirstAndCodeFirst;

public partial class EtradeContext : DbContext
{
    public EtradeContext()
    {
    }

    public EtradeContext(DbContextOptions<EtradeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Invoicedetail> Invoicedetails { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemsLog> ItemsLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Reports2> Reports2s { get; set; }

    public virtual DbSet<Town> Towns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwSiparisDetail> VwSiparisDetails { get; set; }

    public virtual DbSet<VwTop10> VwTop10s { get; set; }

    public virtual DbSet<Vworder> Vworders { get; set; }

    public virtual DbSet<Vworders2> Vworders2s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("ADDRESS");

            entity.HasIndex(e => e.Userid, "IX1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Addresstext)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADDRESSTEXT");
            entity.Property(e => e.Cityid).HasColumnName("CITYID");
            entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");
            entity.Property(e => e.Districtid).HasColumnName("DISTRICTID");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("POSTALCODE");
            entity.Property(e => e.Townid).HasColumnName("TOWNID");
            entity.Property(e => e.Userid).HasColumnName("USERID");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("CITIES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("COUNTRIES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Country1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("DISTRICTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.District1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Townid).HasColumnName("TOWNID");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("INVOICES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Addressid).HasColumnName("ADDRESSID");
            entity.Property(e => e.Cargoficheno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARGOFICHENO");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE_");
            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Totalprice)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TOTALPRICE");
        });

        modelBuilder.Entity<Invoicedetail>(entity =>
        {
            entity.ToTable("INVOICEDETAILS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.Invoiceid).HasColumnName("INVOICEID");
            entity.Property(e => e.Itemid).HasColumnName("ITEMID");
            entity.Property(e => e.Linetotal)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("LINETOTAL");
            entity.Property(e => e.Orderdetailid).HasColumnName("ORDERDETAILID");
            entity.Property(e => e.Unitprice)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("UNITPRICE");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("ITEMS", tb =>
                {
                    tb.HasTrigger("ITEMS_DELETE_AND_UPDATE");
                    tb.HasTrigger("TRG_INSTEADOF");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BRAND");
            entity.Property(e => e.Category1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY1");
            entity.Property(e => e.Category2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY2");
            entity.Property(e => e.Category3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY3");
            entity.Property(e => e.Category4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY4");
            entity.Property(e => e.Itemcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ITEMCODE");
            entity.Property(e => e.Itemname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ITEMNAME");
            entity.Property(e => e.Unitprice).HasColumnName("UNITPRICE");
        });

        modelBuilder.Entity<ItemsLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ITEMS_LOG");

            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BRAND");
            entity.Property(e => e.Category1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY1");
            entity.Property(e => e.Category2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY2");
            entity.Property(e => e.Category3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY3");
            entity.Property(e => e.Category4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY4");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ITEMCODE");
            entity.Property(e => e.Itemname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ITEMNAME");
            entity.Property(e => e.LogActiontype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOG_ACTIONTYPE");
            entity.Property(e => e.LogDate)
                .HasColumnType("datetime")
                .HasColumnName("LOG_DATE");
            entity.Property(e => e.LogHostname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOG_HOSTNAME");
            entity.Property(e => e.LogProgramname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOG_PROGRAMNAME");
            entity.Property(e => e.LogUsername)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOG_USERNAME");
            entity.Property(e => e.Unitprice).HasColumnName("UNITPRICE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Addressid).HasColumnName("ADDRESSID");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE_");
            entity.Property(e => e.Status).HasColumnName("STATUS_");
            entity.Property(e => e.Totalprice)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TOTALPRICE");
            entity.Property(e => e.Userid).HasColumnName("USERID");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.ToTable("ORDERDETAILS");

            entity.HasIndex(e => e.Itemid, "IX1").HasFillFactor(70);

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.Itemid).HasColumnName("ITEMID");
            entity.Property(e => e.Linetotal)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("LINETOTAL");
            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Unitprice)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("UNITPRICE");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("PAYMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Approvecode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APPROVECODE");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE_");
            entity.Property(e => e.Isok).HasColumnName("ISOK");
            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Paymenttotal)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("PAYMENTTOTAL");
            entity.Property(e => e.Paymenttype).HasColumnName("PAYMENTTYPE");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("REPORTS");

            entity.Property(e => e.Ay).HasColumnName("AY");
            entity.Property(e => e.Musterisayisi).HasColumnName("MUSTERISAYISI");
            entity.Property(e => e.Toplammiktar).HasColumnName("TOPLAMMIKTAR");
            entity.Property(e => e.Toplamtutar)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TOPLAMTUTAR");
            entity.Property(e => e.Urunsayisi).HasColumnName("URUNSAYISI");
            entity.Property(e => e.Yil).HasColumnName("YIL");
        });

        modelBuilder.Entity<Reports2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("REPORTS2");

            entity.Property(e => e.Ay)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("AY");
            entity.Property(e => e.Musterisayisi).HasColumnName("MUSTERISAYISI");
            entity.Property(e => e.Toplammiktar).HasColumnName("TOPLAMMIKTAR");
            entity.Property(e => e.Toplamtutar)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TOPLAMTUTAR");
            entity.Property(e => e.Urunsayisi).HasColumnName("URUNSAYISI");
            entity.Property(e => e.Yil).HasColumnName("YIL");
        });

        modelBuilder.Entity<Town>(entity =>
        {
            entity.ToTable("TOWNS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Cityid).HasColumnName("CITYID");
            entity.Property(e => e.Town1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOWN");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthdate).HasColumnName("BIRTHDATE");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.Namesurname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAMESURNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_");
            entity.Property(e => e.Telnr1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELNR1");
            entity.Property(e => e.Telnr2)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELNR2");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME_");
        });

        modelBuilder.Entity<VwSiparisDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_SIPARIS_DETAILS");

            entity.Property(e => e.Adres)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADRES");
            entity.Property(e => e.Adsoyad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADSOYAD");
            entity.Property(e => e.Ay)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("AY");
            entity.Property(e => e.Birimfiyat)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("BIRIMFIYAT");
            entity.Property(e => e.Ilce)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ILCE");
            entity.Property(e => e.Kategori)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KATEGORI");
            entity.Property(e => e.Kategori2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KATEGORI2");
            entity.Property(e => e.Kullaniciadi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KULLANICIADI");
            entity.Property(e => e.Miktar).HasColumnName("MIKTAR");
            entity.Property(e => e.Satirtoplami)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("SATIRTOPLAMI");
            entity.Property(e => e.Sehir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SEHIR");
            entity.Property(e => e.Siparisid).HasColumnName("SIPARISID");
            entity.Property(e => e.Siparistarih)
                .HasColumnType("datetime")
                .HasColumnName("SIPARISTARIH");
            entity.Property(e => e.Siparistarihi).HasColumnName("SIPARISTARIHI");
            entity.Property(e => e.Siparisyili).HasColumnName("SIPARISYILI");
            entity.Property(e => e.Siprariszamani).HasColumnName("SIPRARISZAMANI");
            entity.Property(e => e.Telefonnumara)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONNUMARA");
            entity.Property(e => e.Ulke)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ULKE");
            entity.Property(e => e.Urunadi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("URUNADI");
            entity.Property(e => e.Urunkodu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNKODU");
            entity.Property(e => e.Urunmarka)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNMARKA");
        });

        modelBuilder.Entity<VwTop10>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_TOP_10");

            entity.Property(e => e.Kategori)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KATEGORI");
            entity.Property(e => e.Toplamtutar)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TOPLAMTUTAR");
        });

        modelBuilder.Entity<Vworder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VWORDERS");

            entity.Property(e => e.Adres)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADRES");
            entity.Property(e => e.Adsoyad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADSOYAD");
            entity.Property(e => e.Birimfiyat)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("BIRIMFIYAT");
            entity.Property(e => e.Ilce)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ILCE");
            entity.Property(e => e.Kullaniciadi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KULLANICIADI");
            entity.Property(e => e.Miktar).HasColumnName("MIKTAR");
            entity.Property(e => e.Satirtoplami)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("SATIRTOPLAMI");
            entity.Property(e => e.Sehir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SEHIR");
            entity.Property(e => e.Siparisid).HasColumnName("SIPARISID");
            entity.Property(e => e.Siparistarih)
                .HasColumnType("datetime")
                .HasColumnName("SIPARISTARIH");
            entity.Property(e => e.Ulke)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ULKE");
            entity.Property(e => e.Urunadi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("URUNADI");
            entity.Property(e => e.Urunkodu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNKODU");
            entity.Property(e => e.Urunmarka)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNMARKA");
        });

        modelBuilder.Entity<Vworders2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VWORDERS2");

            entity.Property(e => e.Adres)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADRES");
            entity.Property(e => e.Adsoyad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADSOYAD");
            entity.Property(e => e.Birimfiyat)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("BIRIMFIYAT");
            entity.Property(e => e.Ilce)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ILCE");
            entity.Property(e => e.Kullaniciadi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KULLANICIADI");
            entity.Property(e => e.Miktar).HasColumnName("MIKTAR");
            entity.Property(e => e.Satirtoplami)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("SATIRTOPLAMI");
            entity.Property(e => e.Sehir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SEHIR");
            entity.Property(e => e.Siparisid).HasColumnName("SIPARISID");
            entity.Property(e => e.Siparistarih)
                .HasColumnType("datetime")
                .HasColumnName("SIPARISTARIH");
            entity.Property(e => e.Ulke)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ULKE");
            entity.Property(e => e.Urunadi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("URUNADI");
            entity.Property(e => e.Urunkodu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNKODU");
            entity.Property(e => e.Urunmarka)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUNMARKA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

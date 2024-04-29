using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class VwSiparisDetail
{
    public string? Adsoyad { get; set; }

    public string? Kullaniciadi { get; set; }

    public string? Telefonnumara { get; set; }

    public string? Ulke { get; set; }

    public string? Sehir { get; set; }

    public string? Ilce { get; set; }

    public string? Adres { get; set; }

    public int Siparisid { get; set; }

    public DateTime? Siparistarih { get; set; }

    public string? Urunkodu { get; set; }

    public string? Urunadi { get; set; }

    public string? Urunmarka { get; set; }

    public string? Kategori { get; set; }

    public string? Kategori2 { get; set; }

    public int? Miktar { get; set; }

    public decimal? Birimfiyat { get; set; }

    public decimal? Satirtoplami { get; set; }

    public DateOnly? Siprariszamani { get; set; }

    public TimeOnly? Siparistarihi { get; set; }

    public int? Siparisyili { get; set; }

    public string? Ay { get; set; }
}

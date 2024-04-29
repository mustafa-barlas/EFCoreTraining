using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class Payment
{
    public int Id { get; set; }

    public int? Orderid { get; set; }

    public byte? Paymenttype { get; set; }

    public DateTime? Date { get; set; }

    public bool? Isok { get; set; }

    public string? Approvecode { get; set; }

    public decimal? Paymenttotal { get; set; }
}

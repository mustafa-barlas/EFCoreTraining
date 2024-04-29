using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class Invoice
{
    public int Id { get; set; }

    public int? Orderid { get; set; }

    public DateTime? Date { get; set; }

    public int? Addressid { get; set; }

    public string? Cargoficheno { get; set; }

    public decimal? Totalprice { get; set; }
}

using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class Orderdetail
{
    public int Id { get; set; }

    public int? Orderid { get; set; }

    public int? Itemid { get; set; }

    public int? Amount { get; set; }

    public decimal? Unitprice { get; set; }

    public decimal? Linetotal { get; set; }
}

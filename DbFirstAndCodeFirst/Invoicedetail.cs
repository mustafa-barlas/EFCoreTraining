using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class Invoicedetail
{
    public int Id { get; set; }

    public int? Invoiceid { get; set; }

    public int? Orderdetailid { get; set; }

    public int? Itemid { get; set; }

    public int? Amount { get; set; }

    public decimal? Unitprice { get; set; }

    public decimal? Linetotal { get; set; }
}

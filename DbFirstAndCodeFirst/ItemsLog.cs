using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class ItemsLog
{
    public int Id { get; set; }

    public string? Itemcode { get; set; }

    public string? Itemname { get; set; }

    public double? Unitprice { get; set; }

    public string? Category1 { get; set; }

    public string? Category2 { get; set; }

    public string? Category3 { get; set; }

    public string? Category4 { get; set; }

    public string? Brand { get; set; }

    public string? LogActiontype { get; set; }

    public DateTime? LogDate { get; set; }

    public string? LogUsername { get; set; }

    public string? LogProgramname { get; set; }

    public string? LogHostname { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class District
{
    public int Id { get; set; }

    public int? Townid { get; set; }

    public string? District1 { get; set; }
}

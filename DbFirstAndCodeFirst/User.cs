using System;
using System.Collections.Generic;

namespace DbFirstAndCodeFirst;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Namesurname { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthdate { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Telnr1 { get; set; }

    public string? Telnr2 { get; set; }
}

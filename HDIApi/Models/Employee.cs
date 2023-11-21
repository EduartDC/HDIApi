using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Employee
{
    public string IdEmployee { get; set; } = null!;

    public string? NameEmployee { get; set; }

    public string? LastnameEmployee { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Accident> Accidents { get; } = new List<Accident>();
}

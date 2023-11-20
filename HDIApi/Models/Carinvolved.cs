using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Carinvolved
{
    public string IdCarInvolved { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Color { get; set; }

    public string? Plate { get; set; }

    public virtual ICollection<Involved> Involveds { get; } = new List<Involved>();
}

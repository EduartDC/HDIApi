using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Opinionadjuster
{
    public string IdOpinionAdjuster { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Accident> Accidents { get; } = new List<Accident>();
}

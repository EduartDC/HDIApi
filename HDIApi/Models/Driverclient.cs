using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Driverclient
{
    public string IdDriverClient { get; set; } = null!;

    public string? NameDriver { get; set; }

    public string? LastNameDriver { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Password { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Accident> Accidents { get; } = new List<Accident>();

    public virtual ICollection<Insurancepolicy> Insurancepolicies { get; } = new List<Insurancepolicy>();
}

using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Vehicleclient
{
    public string IdVehicleClient { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Color { get; set; }

    public string? Model { get; set; }

    public string? Plate { get; set; }

    public string? SerialNumber { get; set; }

    public string? Year { get; set; }

    public virtual ICollection<Accident> Accidents { get; } = new List<Accident>();

    public virtual ICollection<Insurancepolicy> Insurancepolicies { get; } = new List<Insurancepolicy>();
}

using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Insurancepolicy
{
    public string IdInsurancePolicy { get; set; } = null!;

    public DateTime? StartTerm { get; set; }

    public DateTime? EndTerm { get; set; }

    public int? TermAmount { get; set; }

    public double? Price { get; set; }

    public string? PolicyType { get; set; }

    public string DriverClientIdDriverClient { get; set; } = null!;

    public virtual Driverclient DriverClientIdDriverClientNavigation { get; set; } = null!;

    public virtual ICollection<Vehicleclient> Vehicleclients { get; } = new List<Vehicleclient>();
}

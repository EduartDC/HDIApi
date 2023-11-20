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

    public string InsurancePolicyIdInsurancePolicy { get; set; } = null!;

    public virtual ICollection<Accident> Accidents { get; } = new List<Accident>();

    public virtual Insurancepolicy InsurancePolicyIdInsurancePolicyNavigation { get; set; } = null!;
}

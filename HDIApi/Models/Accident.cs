using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Accident
{
    public string IdAccident { get; set; } = null!;

    public string? Location { get; set; }

    public string? NameLocation { get; set; }

    public string? ReportStatus { get; set; }

    public DateTime? AccidentDate { get; set; }

    public string VehicleClientIdVehicleClient { get; set; } = null!;

    public string DriverClientIdDriverClient { get; set; } = null!;

    public string EmployeeIdEmployee { get; set; } = null!;

    public string OpinionAdjusterIdOpinionAdjuster { get; set; } = null!;

    public virtual Driverclient DriverClientIdDriverClientNavigation { get; set; } = null!;

    public virtual Employee EmployeeIdEmployeeNavigation { get; set; } = null!;

    public virtual ICollection<Image> Images { get; } = new List<Image>();

    public virtual ICollection<Involved> Involveds { get; } = new List<Involved>();

    public virtual Opinionadjuster OpinionAdjusterIdOpinionAdjusterNavigation { get; set; } = null!;

    public virtual Vehicleclient VehicleClientIdVehicleClientNavigation { get; set; } = null!;
}

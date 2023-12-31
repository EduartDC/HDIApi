using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Involved
{
    public string IdInvolved { get; set; } = null!;

    public string? LastNameInvolved { get; set; }

    public string? NameInvolved { get; set; }

    public string? LicenseNumber { get; set; }

    public string AccidentIdAccident { get; set; } = null!;

    public string? CarInvolvedIdCarInvolved { get; set; }

    public virtual Accident AccidentIdAccidentNavigation { get; set; } = null!;

    public virtual Carinvolved? CarInvolvedIdCarInvolvedNavigation { get; set; } = null!;

}

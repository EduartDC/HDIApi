using System;
using System.Collections.Generic;

namespace HDIApi.Models;

public partial class Image
{
    public string Idimages { get; set; } = null!;

    public byte[]? ImageReport { get; set; }

    public string AccidentIdAccident { get; set; } = null!;

    public virtual Accident AccidentIdAccidentNavigation { get; set; } = null!;
}

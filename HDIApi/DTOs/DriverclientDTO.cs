using HDIApi.Models;

namespace HDIApi.DTOs
{
    public class DriverclientDTO
    {
        public string IdDriverClient { get; set; } = null!;

        public string? NameDriver { get; set; }

        public string? LastNameDriver { get; set; }
        public string? AgeString {get;set;}
        public DateOnly Age {get;set;}

        public string FullNameDriver { get; set; } = null!;

        public string? TelephoneNumber { get; set; }

        public string? LicenseNumber { get; set; }

        public string? Password { get; set; }

    }
}

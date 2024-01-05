namespace HDIApi.DTOs
{
    public class AccidentDTO
    {
        public string IdAccident { get; set; }

        public string? Location { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? NameLocation { get; set; }
        public string? ReportStatus { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string DriverClientIdDriverClient { get; set; }
        public string? EmployeeIdEmployee { get; set; }
        public string? OpinionAdjusterIdOpinionAdjuster { get; set; }
        public string VehicleClientIdVehicleClient { get; set; }        
    }
}

namespace HDIApi.DTOs
{
    public class PreviewReportDTO
    {
        public string ReportNumber { get; set; }
        public string NameClient { get; set; } 
        public string StatusReport { get; set; }
        public DateTime ReportDate {get;set;}
        public string Latitude {get;set;}
        public string Longitude {get;set;}

    }
}

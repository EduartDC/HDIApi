namespace HDIApi.DTOs
{
    public class InsurancePolicyDTO
    {
        public string IdInsurancePolicy { get; set; }
        public DateTime StartTerm { get; set; }
        public DateTime EndTerm { get; set; }
        public decimal TermAmount { get; set; }
        public decimal Price { get; set; }
        public string PolicyType { get; set; }
        public string DriverClientIdDriverClient { get; set; }
        public string VehicleClientIdVehicleClient { get; set; }
    }
}

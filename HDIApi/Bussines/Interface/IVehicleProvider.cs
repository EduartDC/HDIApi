namespace HDIApi.Bussines.Interface
{
    public interface IVehicleProvider
    {
        Task<string> AddCustomerVehicle(string brand, string color, string model, string plate, string serialNumber, string year);
    }
}

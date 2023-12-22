using HDIApi.DTOs;
using HDIApi.Models;

namespace HDIApi.Bussines.Interface
{
    public interface IDriverProvider
    {
        public Task<int> SetNewDriver(DriverclientDTO newDriverClient);
    }
}

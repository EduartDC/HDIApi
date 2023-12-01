using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IAccidentProvider
    {
        Task<IEnumerable<AccidentDTO>> GetAccidentsByClient(string idClientDriver);
    }
}

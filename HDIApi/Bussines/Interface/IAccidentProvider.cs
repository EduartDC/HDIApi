using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IAccidentProvider
    {
        Task<IEnumerable<AccidentDTO>> GetAccidents();
        Task<IEnumerable<AccidentDTO>> GetAccidentsWithoutAdjuster();
        Task<bool> AssignAdjusterToAccident(string accidentId, string employeeId);        
    }
}

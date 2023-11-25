using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IReportProvider
    {
        Task<NewReportDTO> GetReportByDriver(string idDriver);
        Task<NewReportDTO> CreateReport(NewReportDTO report);
    }
}

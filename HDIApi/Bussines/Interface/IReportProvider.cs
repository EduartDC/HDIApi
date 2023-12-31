using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IReportProvider
    {
        public Task<NewReportDTO> GetReportByDriver(string idDriver);
        public Task<NewReportDTO> CreateReport(NewReportDTO report);
        public Task<ReportDTO> GetReportById(string idReport);
         public Task<(int, List<PreviewReportDTO>)> GetPreviewReportsByEmployee(string idEmployee);
    }
}

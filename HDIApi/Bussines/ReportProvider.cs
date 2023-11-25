using HDIApi.Bussines.Interface;
using HDIApi.DTOs;

namespace HDIApi.Bussines
{
    public class ReportProvider : IReportProvider
    {
        public Task<NewReportDTO> CreateReport(NewReportDTO report)
        {
            throw new NotImplementedException();
        }

        public Task<NewReportDTO> GetReportByDriver(string idDriver)
        {
            throw new NotImplementedException();
        }
    }
}

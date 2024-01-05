using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IReportProvider
    {
        public Task<bool> CreateReport(NewReportDTO report);
        public Task<ReportDTO> GetReportById(string idReport);

        public Task<(int, List<PreviewReportDTO>)> GetPreviewReportsByEmployee(string idEmployee);

        public Task<bool> PutOpinion(NewOpinionadjusterDTO opinion);
        public Task<bool> PostOpinion(NewOpinionadjusterDTO opinion);

    }
}

using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IReportProvider
    {
        public Task<NewReportDTO> GetReportByDriver(string idDriver);
        public Task<NewReportDTO> CreateReport(NewReportDTO report);
        public Task<ReportDTO> GetReportById(string idReport);
        public Task<bool> CreateNewOpinion(NewOpinionadjusterDTO opinion);
        public Task<bool> UpdateOpinion(OpinionadjusterDTO opinion);
        public Task<bool> PutOpinion(NewOpinionadjusterDTO opinion);
        public Task<bool> PostOpinion(NewOpinionadjusterDTO opinion);
    }
}

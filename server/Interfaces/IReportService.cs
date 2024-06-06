using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Interfaces
{
    public interface IReportService
    {
        Task<QueryResponse<ReportWorkDto>> GetReportWork(ReportWorkQuery query, string permission, long idUser, List<long> idEmployees);
    }
}

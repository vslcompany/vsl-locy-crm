namespace vsl_crm_api.Models.Requests
{
    public class DeleteEmployeeRequest
    {
        public required long IdNhanVien { get; set; }
        public required bool FlagDelete { get; set; }
        public long? IdUserDelete { get; set; }
    }
}

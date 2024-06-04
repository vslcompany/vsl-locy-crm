namespace vsl_crm_api.Models.Requests
{
    public class CreateEmployeeGroupRequest
    {
        public long ParentId { get; set; }
        public string? NameGroup { get; set; }
        public required long[] IdNhanVien { get; set; }
        public bool? FlagViewAllGroup { get; set; }
    }
}

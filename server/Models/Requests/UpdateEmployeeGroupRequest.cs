namespace vsl_crm_api.Models.Requests
{
    public class UpdateEmployeeGroupRequest
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string? NameGroup { get; set; }
        public long? IdNhanVien { get; set; }
        public bool? FlagViewAllGroup { get; set; }
    }
}

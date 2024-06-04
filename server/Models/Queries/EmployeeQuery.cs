namespace vsl_crm_api.Models.Queries
{
    public class EmployeeQuery
    {
        // Pagination
        public int Start { get; set; }
        public int Size { get; set; }

        // Filter
        public string? Name { get; set; }
        public long? IdChucVu { get; set; }
        public long? IdVanPhong { get; set; }
        public long? IdPhongBan { get; set; }

        public required bool TrangThai { get; set; }
    }
}

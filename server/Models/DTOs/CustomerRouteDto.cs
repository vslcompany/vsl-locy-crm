namespace vsl_crm_api.Models.DTOs
{
    public class CustomerRouteDto
    {
        public long Id { get; set; }
        public long? IdQuocGiaDi { get; set; }
        public string? QuocGiaDi { get; set; }
        public long? IdQuocGiaDen { get; set; }
        public string? QuocGiaDen { get; set; }
        public long? IdCangDi { get; set; }
        public string? CangDi { get; set; }
        public long? IdCangDen { get; set; }
        public string? CangDen { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdLoaiHinhVanChuyen { get; set; }
        public string? LoaiHinhVanChuyen { get; set; }
    }
}

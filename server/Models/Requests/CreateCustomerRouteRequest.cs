namespace vsl_crm_api.Models.Requests
{
    public class CreateCustomerRouteRequest
    {
        public long? IdQuocGiaDi { get; set; }
        public long? IdQuocGiaDen { get; set; }
        public long? IdCangDi { get; set; }
        public long? IdCangDen { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdLoaiHinhVanChuyen { get; set; }
    }
}

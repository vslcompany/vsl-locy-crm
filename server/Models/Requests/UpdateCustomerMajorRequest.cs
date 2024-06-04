namespace vsl_crm_api.Models.Requests
{
    public class UpdateCustomerMajorRequest
    {
        public long Id { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdNghiepVu { get; set; }
    }
}

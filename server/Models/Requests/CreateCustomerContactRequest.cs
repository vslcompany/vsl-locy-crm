namespace vsl_crm_api.Models.Requests
{
    public class CreateCustomerContactRequest
    {
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? AddressVI { get; set; }
        public string? AddressEN { get; set; }
        public int? EnumGioiTinh { get; set; }
        public string? HandPhone { get; set; }
        public string? HomePhone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public long? IdCustomer { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankBranchName { get; set; }
        public string? BankAddress { get; set; }
        public string? Chat { get; set; }
        public bool? FlagDaiDien { get; set; }
        public string? ChucVu { get; set; }
    }
}

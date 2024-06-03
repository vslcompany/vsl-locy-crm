namespace vsl_crm_api.Models.Errors
{
    public class ErrorResponse
    {
        public Boolean status { get; set; }
        public int statusCode { get; set; }
        public string title { get; set; } = "";
        public string message { get; set; } = "";
    }
}

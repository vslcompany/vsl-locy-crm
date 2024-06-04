namespace vsl_crm_api.Models.Responses
{
    public class Response
    {
        public Boolean Status { get; set; }
        public string Message { get; set; } = "";
        public Object? Data { get; set; } = null;
    }
}

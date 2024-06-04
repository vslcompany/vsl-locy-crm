namespace vsl_crm_api.Models.Responses
{
    public class QueryResponse<T>
    {
        public List<T>? data { get; set; }
        public int totalRow { get; set; }
    }
}

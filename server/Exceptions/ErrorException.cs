namespace vsl_crm_api.Exceptions
{
    public class ErrorException: Exception
    {
        public int statusCode { get; }
        public string title { get; } = "";

        public ErrorException(int StatusCode, string Title, string message) : base(message)
        {
            statusCode = StatusCode;
            title = Title;
        }
    }
}

namespace vsl_crm_api.Models.Queries
{
    public class ReportWorkQuery
    {
        // Pagination
        public int Start { get; set; }
        public int Size { get; set; }

        // Condition
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
    }
}

namespace ReportingSystem.Reports
{
    public class SalesReport : Report, ISummarizable
    {
        public SalesReport() : base("Sales Report") { }

        public override string GenerateContent()
        {
            return "Detailed sales data...";
        }

        public string GenerateSummary()
        {
            return "Summary: Total sales = 1M USD";
        }
    }
}

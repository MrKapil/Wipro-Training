using ReportingSystem.Reports;

namespace ReportingSystem.Generation
{
    public interface IReportGenerator
    {
        string Generate(IReport report);
    }

    public class ReportGenerator : IReportGenerator
    {
        public string Generate(IReport report)
        {
            return $"Title: {report.Title}\nContent:\n{report.GenerateContent()}";
        }
    }
}

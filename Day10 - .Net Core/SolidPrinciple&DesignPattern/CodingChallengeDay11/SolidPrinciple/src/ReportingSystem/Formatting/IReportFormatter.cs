namespace ReportingSystem.Formatting
{
    public interface IReportFormatter
    {
        string Format(string content);
    }

    public class PdfReportFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"[PDF FORMAT]\n{content}";
        }
    }

    public class ExcelReportFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"[Excel FORMAT]\n{content}";
        }
    }
}

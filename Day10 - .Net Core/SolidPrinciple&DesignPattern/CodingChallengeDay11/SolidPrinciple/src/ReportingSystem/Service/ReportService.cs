using ReportingSystem.Generation;
using ReportingSystem.Formatting;
using ReportingSystem.Reports;

namespace ReportingSystem.Service
{
    public class ReportService
    {
        private readonly IReportGenerator _generator;
        private readonly IReportSaver _saver;
        private readonly IReportFormatter _formatter;

        public ReportService(IReportGenerator generator, IReportSaver saver, IReportFormatter formatter)
        {
            _generator = generator;
            _saver = saver;
            _formatter = formatter;
        }

        public void CreateAndSaveReport(IReport report, string filePath)
        {
            var content = _generator.Generate(report);
            var formatted = _formatter.Format(content);
            _saver.Save(formatted, filePath);
        }
    }
}

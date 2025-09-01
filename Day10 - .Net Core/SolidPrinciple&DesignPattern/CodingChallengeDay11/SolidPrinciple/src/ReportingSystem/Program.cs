using ReportingSystem.Generation;
using ReportingSystem.Formatting;
using ReportingSystem.Reports;
using ReportingSystem.Service;

namespace ReportingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var generator = new ReportGenerator();
            var saver = new ReportSaver();
            IReportFormatter formatter = new PdfReportFormatter(); // could switch to ExcelReportFormatter

            var service = new ReportService(generator, saver, formatter);

            IReport report1 = new SalesReport();
            IReport report2 = new InventoryReport();

            service.CreateAndSaveReport(report1, "SalesReport.txt");
            service.CreateAndSaveReport(report2, "InventoryReport.txt");

            Console.WriteLine("Reports generated and saved successfully!");
        }
    }
}

using ReportingSystem.Generation;
using ReportingSystem.Formatting;
using ReportingSystem.Reports;
using ReportingSystem.Service;
using Xunit;

public class ReportServiceTests
{
    [Fact]
    public void Service_Saves_File()
    {
        var generator = new ReportGenerator();
        var saver = new ReportSaver();
        var formatter = new PdfReportFormatter();

        var service = new ReportService(generator, saver, formatter);

        var filePath = Path.GetTempFileName();
        service.CreateAndSaveReport(new SalesReport(), filePath);

        var content = File.ReadAllText(filePath);
        Assert.Contains("[PDF FORMAT]", content);
        Assert.Contains("Sales Report", content);
    }
}

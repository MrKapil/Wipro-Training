using ReportingSystem.Generation;
using ReportingSystem.Reports;
using Xunit;

public class ReportGeneratorTests
{
    [Fact]
    public void Generator_Returns_Content()
    {
        var generator = new ReportGenerator();
        var report = new SalesReport();
        var result = generator.Generate(report);

        Assert.Contains("Sales Report", result);
        Assert.Contains("Detailed sales data", result);
    }
}

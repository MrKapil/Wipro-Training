using ReportingSystem.Formatting;
using Xunit;

public class FormatterTests
{
    [Fact]
    public void PdfFormatter_AddsPdfTag()
    {
        var formatter = new PdfReportFormatter();
        var result = formatter.Format("content");
        Assert.StartsWith("[PDF FORMAT]", result);
    }

    [Fact]
    public void ExcelFormatter_AddsExcelTag()
    {
        var formatter = new ExcelReportFormatter();
        var result = formatter.Format("content");
        Assert.StartsWith("[Excel FORMAT]", result);
    }
}

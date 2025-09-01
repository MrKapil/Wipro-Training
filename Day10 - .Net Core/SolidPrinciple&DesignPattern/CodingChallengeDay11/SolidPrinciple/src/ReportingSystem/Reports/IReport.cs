namespace ReportingSystem.Reports
{
    
    public interface IReport
    {
        string Title { get; }
        string GenerateContent();
    }

    
    public interface ISummarizable
    {
        string GenerateSummary();
    }
}

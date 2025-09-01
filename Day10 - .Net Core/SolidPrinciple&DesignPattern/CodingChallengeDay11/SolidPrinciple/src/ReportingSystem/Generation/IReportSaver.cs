namespace ReportingSystem.Generation
{
    public interface IReportSaver
    {
        void Save(string content, string filePath);
    }

    public class ReportSaver : IReportSaver
    {
        public void Save(string content, string filePath)
        {
            File.WriteAllText(filePath, content);
        }
    }
}

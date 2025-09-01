namespace ReportingSystem.Reports
{
    public abstract class Report : IReport
    {
        public string Title { get; protected set; }

        protected Report(string title)
        {
            Title = title;
        }

        public abstract string GenerateContent();
    }
}

namespace ReportingSystem.Reports
{
    public class InventoryReport : Report
    {
        public InventoryReport() : base("Inventory Report") { }

        public override string GenerateContent()
        {
            return "Inventory details with quantities and stock levels...";
        }
    }
}

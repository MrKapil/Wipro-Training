namespace ShopForHome.Api.Entities
{
    public class Inventory
    {
        public long ProductId { get; set; }  // PK and FK to Product
        public int StockQty { get; set; }

        public Product? Product { get; set; }
    }
}

using System.Text.Json;
using EcommerceApp.Models;

namespace EcommerceApp.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _ctx;
        private const string CartKey = "cart";

        public CartService(IHttpContextAccessor? accessor = null)
        {
            _ctx = accessor ?? new HttpContextAccessor();
        }

        private ISession Session => _ctx.HttpContext!.Session;

        public List<CartItem> GetCart()
        {
            var json = Session.GetString(CartKey);
            return json is null ? new List<CartItem>() :
                   (JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>());
        }

        private void SaveCart(List<CartItem> items) =>
            Session.SetString(CartKey, JsonSerializer.Serialize(items));

        public void Clear() => Session.Remove(CartKey);

        public void Add(Product product, int quantity = 1)
        {
            var items = GetCart();
            var existing = items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing is null)
            {
                items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    UnitPrice = product.Price,
                    Quantity = Math.Clamp(quantity, 1, 99)
                });
            }
            else
            {
                existing.Quantity = Math.Clamp(existing.Quantity + quantity, 1, 99);
            }
            SaveCart(items);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var items = GetCart();
            var it = items.FirstOrDefault(i => i.ProductId == productId);
            if (it != null)
            {
                it.Quantity = Math.Clamp(quantity, 1, 99);
                SaveCart(items);
            }
        }

        public void Remove(int productId)
        {
            var items = GetCart();
            items.RemoveAll(i => i.ProductId == productId);
            SaveCart(items);
        }
    }
}

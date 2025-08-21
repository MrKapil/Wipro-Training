using System.Collections.Generic;

namespace RazerPageApplication.Services
{
    public class ItemStore
    {
        private readonly List<Item> _items = new();
        private int _nextId = 1;

        public IReadOnlyList<Item> All => _items;

        public void Add(Item item)
        {
            item.Id = _nextId++;
            _items.Add(item);
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

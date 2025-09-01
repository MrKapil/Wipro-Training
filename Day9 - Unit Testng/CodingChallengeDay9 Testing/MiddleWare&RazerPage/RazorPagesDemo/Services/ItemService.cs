using System.Collections.Generic;

public class ItemService
{
    private readonly List<string> _items = new();

    public IEnumerable<string> GetItems() => _items;

    public void AddItem(string item)
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            _items.Add(item);
        }
    }
}

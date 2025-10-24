using System.Collections.Generic;
using UnityEngine;

//temp inven
namespace Fragments.Runtime
{
    public class SimpleInventory : MonoBehaviour, IInventory
    {
        readonly Dictionary<string, int> _items = new();

        public bool TryAdd(string resourceId, int amount)
        {
            if (string.IsNullOrWhiteSpace(resourceId) || amount <= 0) return false;
            _items.TryGetValue(resourceId, out int cur);
            _items[resourceId] = cur + amount;
            Debug.Log($"[Inventory] +{amount} {resourceId} (total: {_items[resourceId]})");
            return true;
        }
    }
}

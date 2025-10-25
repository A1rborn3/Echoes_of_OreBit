using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int count;

    public InventoryItem(string name, int count)
    {
        itemName = name;
        this.count = count;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    
    private Dictionary<string, InventoryItem> itemDictionary = new Dictionary<string, InventoryItem>();
    public int credits;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void AddItem(string itemName, int amount = 1)
    {
        if (itemDictionary.ContainsKey(itemName))
        {
            itemDictionary[itemName].count += amount;
        }
        else
        {
            itemDictionary[itemName] = new InventoryItem(itemName, amount);
        }

        Debug.Log($"Added {amount}x {itemName} to inventory (Total: {itemDictionary[itemName].count})");
    }

    
    public void RemoveItem(string itemName, int amount = 1)
    {
        if (itemDictionary.ContainsKey(itemName))
        {
            itemDictionary[itemName].count -= amount;

            if (itemDictionary[itemName].count <= 0)
            {
                itemDictionary.Remove(itemName);
                Debug.Log($"Removed all of {itemName} from inventory");
            }
            else
            {
                Debug.Log($"Removed {amount}x {itemName}. Remaining: {itemDictionary[itemName].count}");
            }
        }
    }

    
    public int GetItemCount(string itemName)
    {
        return itemDictionary.ContainsKey(itemName) ? itemDictionary[itemName].count : 0;
    }

    
    public void AddCredits(int amount)
    {
        credits += amount;
        Debug.Log($"Credits: {credits}");
    }

    public void SpendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            Debug.Log($"Spent {amount} credits. Remaining: {credits}");
        }
        else
        {
            Debug.LogWarning("Not enough credits!");
        }
    }

    
    public InventoryData GetInventoryData()
    {
        InventoryData data = new InventoryData();
        data.items = new List<string>();
        data.itemCounts = new List<int>();
        data.Credits = credits;

        foreach (var pair in itemDictionary)
        {
            data.items.Add(pair.Key);
            data.itemCounts.Add(pair.Value.count);
        }

        return data;
    }

    
    public void LoadInventoryData(InventoryData data)
    {
        itemDictionary.Clear();

        for (int i = 0; i < data.items.Count; i++)
        {
            string itemName = data.items[i];
            int count = data.itemCounts[i];
            itemDictionary[itemName] = new InventoryItem(itemName, count);
        }

        credits = data.Credits;
        Debug.Log("Inventory loaded successfully");
    }


    public IEnumerable<InventoryItem> GetAllItems()
    {
        return itemDictionary.Values;
    }

    // added below for cost
    
    public int GetItemSellPrice(string itemName)
    {
        if (PriceManager.Instance != null)
            return PriceManager.Instance.GetPrice(itemName);
        return 0;
    }

    public int SellItem(string itemName, int amount)
    {
        int owned = GetItemCount(itemName);
        if (owned <= 0) return 0;

        int sellAmount = Mathf.Min(owned, amount);
        int price = GetItemSellPrice(itemName);
        if (price <= 0) return 0;

        RemoveItem(itemName, sellAmount);
        AddCredits(price * sellAmount);

        Debug.Log($"Sold {sellAmount}x {itemName} for {price * sellAmount} credits.");
        return price * sellAmount;
    }

    public int SellAllItems()
    {
        int totalEarned = 0;
        List<string> itemNames = new List<string>();

        foreach (var item in itemDictionary.Values)
            itemNames.Add(item.itemName);

        foreach (var itemName in itemNames)
        {
            int count = GetItemCount(itemName);
            int price = GetItemSellPrice(itemName);
            if (price > 0 && count > 0)
            {
                totalEarned += price * count;
                RemoveItem(itemName, count);
            }
        }

        if (totalEarned > 0)
        {
            AddCredits(totalEarned);
            Debug.Log($"Sold everything for {totalEarned} credits.");
        }

        return totalEarned;
    }
}
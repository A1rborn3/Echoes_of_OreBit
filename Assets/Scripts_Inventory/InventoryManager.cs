using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> items = new List<string>();
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

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Added {itemName} to inventory");
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log($"Removed {itemName} from inventory");
        }
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
        data.items = new List<string>(items);
        data.Credits = credits;
        return data;
    }

    public void LoadInventoryData(InventoryData data)
    {
        items = new List<string>(data.items);
        credits = data.Credits;
        Debug.Log("Inventory loaded successfully");
    }




}

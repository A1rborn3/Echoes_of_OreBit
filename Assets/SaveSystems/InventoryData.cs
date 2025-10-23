using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public List<string> items = new List<string>();
    public List<int> itemCounts = new List<int>();
    public int Credits;
}
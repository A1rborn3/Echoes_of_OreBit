using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== Inventory Test Start ===");

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager is missing in the scene!");
            return;
        }

        InventoryManager.Instance.AddItem("Iron Ore");
        InventoryManager.Instance.AddItem("Fuel Cell");

        InventoryManager.Instance.AddCredits(100);
        InventoryManager.Instance.SpendCredits(30);

        InventoryManager.Instance.RemoveItem("Fuel Cell");

        Debug.Log($"Items left: {string.Join(", ", InventoryManager.Instance.items)}");
        Debug.Log($"Credits left: {InventoryManager.Instance.credits}");

        Debug.Log("=== Inventory Test End ===");


    }




}

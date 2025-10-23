using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    void Start()
    {
        InventoryManager.Instance.AddItem("Iron");
        InventoryManager.Instance.AddItem("Copper");
        InventoryManager.Instance.AddItem("Iron");
        InventoryManager.Instance.AddCredits(345);

    }




}

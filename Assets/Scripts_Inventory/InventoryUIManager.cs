using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotContainer;
    public GameObject panel;

    private void Start()
    {

        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        var data = InventoryManager.Instance.GetInventoryData();

        TextMeshProUGUI Credits = panel.transform.Find("CreditsAmount").GetComponent<TextMeshProUGUI>();
        Credits.text = data.Credits.ToString();

        for (int i = 0; i < data.items.Count; i++)
        {
            string itemName = data.items[i];
            int itemCount = data.itemCounts[i];

            GameObject slot = Instantiate(slotPrefab, slotContainer);

            TextMeshProUGUI nameText = slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI countText = slot.transform.Find("Ammount").GetComponent<TextMeshProUGUI>();

            nameText.text = itemName;
            countText.text = itemCount > 1 ? itemCount.ToString() : "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(!panel.activeSelf);
            if (panel.activeSelf)
                RefreshUI();
        }
    }
}
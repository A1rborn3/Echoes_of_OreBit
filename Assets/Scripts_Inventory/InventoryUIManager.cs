using Fragments.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotContainer;
    public GameObject panel;
    public AreaSpawner2D spawner;

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

            itemName = itemName.Split('(')[0]; //the objects say (clone) at the end so gotta remove it

            GameObject slot = Instantiate(slotPrefab, slotContainer);


            TextMeshProUGUI nameText = slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI countText = slot.transform.Find("Ammount").GetComponent<TextMeshProUGUI>();
            Image iconImage = slot.transform.Find("ItemImage").GetComponent<Image>();



            nameText.text = itemName;
            Debug.Log("name set");
            countText.text = itemCount > 1 ? itemCount.ToString() : "";
            Debug.Log("item count set");
            Sprite iconSprite = GetIconFromSpawner(itemName);
            if (iconSprite != null)
            {
                iconImage.sprite = iconSprite;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
                Debug.LogWarning($"No sprite found for item '{itemName}'");
            }
        }
    }

    private Sprite GetIconFromSpawner(string itemName)
    {
        if (spawner == null || spawner.orePrefabs == null)
            return null;

        foreach (var prefab in spawner.orePrefabs)
        {
            if (prefab == null) continue;

            // Match by prefab name
            if (prefab.name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase))
            {
                // Try to get the sprite from the prefab
                var sr = prefab.GetComponent<SpriteRenderer>();
                if (sr != null)
                    return sr.sprite;

                // Or from a child if needed
                var childSR = prefab.GetComponentInChildren<SpriteRenderer>();
                if (childSR != null)
                    return childSR.sprite;
            }
        }
        return null;
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
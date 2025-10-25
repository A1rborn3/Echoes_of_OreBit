using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PriceManager : MonoBehaviour
{
    public static PriceManager Instance;

    // Case-insensitive keys so "iron" / "Iron" both work
    private Dictionary<string, int> priceLookup =
        new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            RefreshPrices(); // initial scan
            SceneManager.sceneLoaded += OnSceneLoaded; // refresh when new scenes load
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshPrices();
    }

    public void RefreshPrices()
    {
        priceLookup.Clear();

        ItemPrice[] allPrices = FindObjectsOfType<ItemPrice>(true); // include inactive
        foreach (var p in allPrices)
        {
            if (string.IsNullOrWhiteSpace(p.itemName)) continue;

            // last one wins to avoid “duplicate” spam during iterations
            priceLookup[p.itemName] = p.sellPrice;
        }
    }

    public int GetPrice(string itemName)
    {
        if (priceLookup.TryGetValue(itemName, out int price))
            return price;
        return 0;
    }
}

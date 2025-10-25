using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Thrust Upgrade (ButtonOne)")]
    [SerializeField] TMP_Text levelTextOne;
    [SerializeField] Button buttonOne;    


    //added cost for upgrade
    [SerializeField] private int upgradeCost = 100; // upgrade cost per click
    [SerializeField] private int refuelCost = 50;  // refuel cost per click
    [SerializeField] private int ringUpgradeCost = 100; //ring upg cost

    [SerializeField] float thrustAddPerClick = 20f;    // thrust increase per click
    [SerializeField] float slowDownAddPerClick = 20f;  // slowDown increase per click
    [SerializeField] int thrustMaxLevel = 3;           // only 3 clicks allowed

    private const string ThrustLevelKey = "ThrustLevel";
    private const string ThrustBonusKey = "ThrustBonus";
    private const string SlowDownBonusKey = "SlowDownBonus";


    [Header("Ring Count Upgrade (ButtonTwo)")]
    [SerializeField] TMP_Text levelTextTwo;   
    [SerializeField] Button buttonTwo;        

    [SerializeField] int ringAddPerClick = 2; // +2 rings per click
    [SerializeField] int ringMaxLevel = 3;    // only 3 clicks allowed

    private const string RingLevelKey = "RingLevel";
    private const string RingBonusKey = "RingCountBonus";

    public GameObject fueltxt;


    void Start()
    {
        RefreshUI();
    }

    public void UpgradeThrustAndSlowDown()
    {
        if (InventoryManager.Instance.credits < upgradeCost)
        {
            Debug.LogWarning("Not enough credits to upgrade!");
            return;
        }

        InventoryManager.Instance.SpendCredits(upgradeCost); // added for cost

        int level = PlayerPrefs.GetInt(ThrustLevelKey, 0);

        if (level >= thrustMaxLevel) return;   // stop if max level reached

        level++;

        // get current saved values
        float thrustBonus = PlayerPrefs.GetFloat(ThrustBonusKey, 0f);
        float slowDownBonus = PlayerPrefs.GetFloat(SlowDownBonusKey, 0f);

        // apply upgrades
        thrustBonus += thrustAddPerClick;
        slowDownBonus += slowDownAddPerClick;

        // save back
        PlayerPrefs.SetInt(ThrustLevelKey, level);
        PlayerPrefs.SetFloat(ThrustBonusKey, thrustBonus);
        PlayerPrefs.SetFloat(SlowDownBonusKey, slowDownBonus);
        PlayerPrefs.Save();

        Debug.Log($"Upgraded to Level {level} | Thrust bonus: {thrustBonus} | SlowDown bonus: {slowDownBonus}");

        RefreshUI();
    }

    public void UpgradeRingCount()
    {
        // added for cost
        // spend credits before applying upgrade
        if (InventoryManager.Instance.credits < ringUpgradeCost)
        {
            Debug.LogWarning("Not enough credits to upgrade ring count!");
            return;
        }
        InventoryManager.Instance.SpendCredits(ringUpgradeCost);
    
        int level = PlayerPrefs.GetInt(RingLevelKey, 0);
        if (level >= ringMaxLevel) return;

        level++;

        int currentBonus = PlayerPrefs.GetInt(RingBonusKey, 0);
        int newBonus = currentBonus + ringAddPerClick;

        int maxBonus = ringMaxLevel * ringAddPerClick;
        newBonus = Mathf.Min(newBonus, maxBonus);

        PlayerPrefs.SetInt(RingLevelKey, level);
        PlayerPrefs.SetInt(RingBonusKey, newBonus);
        PlayerPrefs.Save();

        RefreshUI();
    }

    public void Refuel()
    {
        if (InventoryManager.Instance.credits < refuelCost)
        {
            Debug.LogWarning("Not enough credits to refuel!");
            return;
        }

        InventoryManager.Instance.SpendCredits(refuelCost); // added above
        
        Data_Transfer.current_fuel_ammount = 200;

        RefreshUI();
    }


    public void ResetAll() 
    {
        PlayerPrefs.SetInt(ThrustLevelKey, 0);
        PlayerPrefs.SetFloat(ThrustBonusKey, 0f);
        PlayerPrefs.SetFloat(SlowDownBonusKey, 0f);

        PlayerPrefs.SetInt(RingLevelKey, 0);
        PlayerPrefs.SetInt(RingBonusKey, 0);

        PlayerPrefs.Save();
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Thrust UI
        int tLevel = PlayerPrefs.GetInt(ThrustLevelKey, 0);
        if (levelTextOne) levelTextOne.text = $"Level {tLevel}";
        if (buttonOne) buttonOne.interactable = tLevel < thrustMaxLevel;

        // Ring UI
        int rLevel = PlayerPrefs.GetInt(RingLevelKey, 0);
        if (levelTextTwo) levelTextTwo.text = $"Level {rLevel}";
        if (buttonTwo) buttonTwo.interactable = rLevel < ringMaxLevel;

        TextMeshProUGUI text = fueltxt.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"fuel = " + Data_Transfer.current_fuel_ammount;
    }

}


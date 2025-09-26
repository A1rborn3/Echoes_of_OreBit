using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Thrust Upgrade (ButtonOne)")]
    [SerializeField] TMP_Text levelTextOne;     
    [SerializeField] Button buttonOne;         

    [SerializeField] float thrustAddPerClick = 20f;    // thrust increase per click
    [SerializeField] float slowDownAddPerClick = 20f;  // slowDown increase per click
    [SerializeField] int maxLevel = 3;                 // only 3 clicks allowed

    private const string ThrustLevelKey = "ThrustLevel";
    private const string ThrustBonusKey = "ThrustBonus";
    private const string SlowDownBonusKey = "SlowDownBonus";

    void Start()
    {
        RefreshUI();
    }

    public void UpgradeThrustAndSlowDown()
    {
        int level = PlayerPrefs.GetInt(ThrustLevelKey, 0);

        if (level >= maxLevel) return;   // stop if max level reached

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

    public void ResetAll() 
    {
        PlayerPrefs.SetInt(ThrustLevelKey, 0);
        PlayerPrefs.SetFloat(ThrustBonusKey, 0f);
        PlayerPrefs.SetFloat(SlowDownBonusKey, 0f);
        PlayerPrefs.Save();
        RefreshUI();
    }

    private void RefreshUI()
    {
        int level = PlayerPrefs.GetInt(ThrustLevelKey, 0);

        if (levelTextOne) levelTextOne.text = $"Level {level}";
        if (buttonOne) buttonOne.interactable = level < maxLevel; 
    }

}


using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Mathematics;


public class StarUIManager : MonoBehaviour
{

    public GameObject travelOverlay;          
    public TextMeshProUGUI systemInfoText;    
    public Button travelButton;
    public TextMeshProUGUI ButtonLabel;
    public int fuel_cost;

    public System_data selectedStarData;
    private string sceneToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //for testing purposes
        Data_Transfer.current_fuel_ammount = 200;
        if (travelOverlay != null)
            travelOverlay.SetActive(false); // hide overlay at start

        if (travelButton != null) {
            travelButton.onClick.AddListener(OnTravelConfirmed);
            ButtonLabel = travelButton.GetComponentInChildren<TextMeshProUGUI>();
        }
            

            
            
    }

    public void ShowStarOverlay(System_data starData, string sceneName) //called on star click
    {
        selectedStarData = starData;
        sceneToLoad = sceneName;
        fuel_cost = (int)Mathf.Round(starData.DistanceFromPlayer * 10);

        if (systemInfoText != null)
        {
            systemInfoText.text = $"System: {starData.name}\nFuel Cost: {fuel_cost}KG\nSeed: {starData.Seed}\n\nCurrent Fuel: {Data_Transfer.current_fuel_ammount}";
        }

        if (fuel_cost > Data_Transfer.current_fuel_ammount)
        {
            ButtonLabel.text = "Not enough fuel";
            ButtonLabel.fontSize = 18;
        }
        else
        {
            ButtonLabel.text = "Warp";
            ButtonLabel.fontSize = 24;
        }

        if (travelOverlay != null)
            travelOverlay.SetActive(true);
    }


    private void OnTravelConfirmed()
    {
        if (CanTravel())
        {
            Data_Transfer.System_ring = selectedStarData.System_Ring;
            Data_Transfer.Star_name = selectedStarData.name;
            Data_Transfer.current_star = selectedStarData.Seed;

            Debug.Log($"Traveling to {selectedStarData.name}, loading {sceneToLoad}...");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public bool CanTravel()
{
    return selectedStarData != null && fuel_cost <= Data_Transfer.current_fuel_ammount;
}
}

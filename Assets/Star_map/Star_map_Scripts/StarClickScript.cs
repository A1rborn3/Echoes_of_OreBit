using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarClickScript : MonoBehaviour
{
    public string sceneToLoad = "Level_test";
    private StarUIManager uiManagerScript;

    void Start()
    {

        uiManagerScript = Object.FindFirstObjectByType<StarUIManager>();
        if (uiManagerScript == null)
            Debug.LogWarning("UI Manager not found in scene!");
    }

    void OnMouseDown()
    {

        System_data data = GetComponent<System_data>();
        if (data == null)
            data = GetComponentInParent<System_data>();

        if (data != null && uiManagerScript != null)
        {
            uiManagerScript.ShowStarOverlay(data, sceneToLoad);
        }
        else
        {
            Debug.LogWarning("System_data or UI Manager not found!");
        }
        
    }
}

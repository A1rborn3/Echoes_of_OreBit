using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarClickScript : MonoBehaviour
{
    public string sceneToLoad = "Level_test"; // name of the scene to load

    void OnMouseDown()
    {
        System_data data = GetComponent<System_data>();
        if (data == null) data = GetComponentInParent<System_data>();

        if (data != null)
        {
            Data_Transfer.System_ring = data.System_Ring; 
            Data_Transfer.Star_name = gameObject.name;
            Data_Transfer.current_star = data.Seed;
        }
        else
        {
            Debug.LogWarning("System_data component not found!");
        }

        // This gets called when the object is clicked
        Debug.Log($"{gameObject.name} clicked. Loading {sceneToLoad}...");
        SceneManager.LoadScene(sceneToLoad);
    }
}

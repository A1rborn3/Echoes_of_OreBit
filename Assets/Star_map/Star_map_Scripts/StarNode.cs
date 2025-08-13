using UnityEngine;

public class StarNode : MonoBehaviour
{
    public string systemName;
    public int systemSeed = 0;
    public float fuelCost;

    public void OnClick()
    {
        // Save selected system data to a GameManager when saving is built
        //GameManager.Instance.SelectedSystemSeed = systemSeed;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_test");
    }
}
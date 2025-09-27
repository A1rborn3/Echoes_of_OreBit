using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public static bool GamePaused = false;
    public GameObject inventoryUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void PauseGame()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
}

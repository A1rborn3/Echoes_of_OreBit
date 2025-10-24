using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToHomeBaseScene()
    {
        ResumeGame();
        SceneManager.LoadScene("Home");
    }
}

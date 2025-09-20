using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeBaseScene : MonoBehaviour
{
    public void ToHomeBaseScene()
    {
        SceneManager.LoadScene("Home");
    }
}

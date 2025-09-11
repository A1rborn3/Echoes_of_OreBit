using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    public void ToHomeScene()
    {
        SceneManager.LoadScene("Home");
    }
}

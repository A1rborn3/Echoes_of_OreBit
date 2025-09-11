using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScene : MonoBehaviour
{
    public void ToUpgradeScene()
    {
        SceneManager.LoadScene("Upgrade");
    }
}

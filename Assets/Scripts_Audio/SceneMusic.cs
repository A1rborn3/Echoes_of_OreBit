using UnityEngine;

public class SceneMusic : MonoBehaviour
{

    [SerializeField] private string musicKey;

    private void Start()
    {
        if (AudioManager.Instance != null && !string.IsNullOrEmpty(musicKey))
        {
            AudioManager.Instance.PlayMusic(musicKey);
        }
        else
        {
            Debug.LogWarning("SceneMusic: No AudioManager found or no musicKey set for this scene.");
        }
    }


}

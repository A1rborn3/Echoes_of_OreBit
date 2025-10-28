using UnityEngine;

public class MouseClickSFX : MonoBehaviour
{
    [SerializeField] private string clickSFXKey = "mouse_click";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance?.PlaySFX(clickSFXKey);
        }
    }
    
}

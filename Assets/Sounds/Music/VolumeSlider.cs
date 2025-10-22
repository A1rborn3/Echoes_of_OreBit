using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null)
            volumeSlider = GetComponent<Slider>();

        if (AudioManager.Instance != null)
        {
            float currentDb;
            if (AudioManager.Instance.audioMixer.GetFloat("MasterVolume", out currentDb))
            {
                float linear = Mathf.Pow(10f, currentDb / 20f);
                volumeSlider.value = linear;
            }
        }

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        AudioManager.Instance?.SetMasterVolume01(value);
    }
}

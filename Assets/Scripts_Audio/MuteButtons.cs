using UnityEngine;
using UnityEngine.UI;

public class MuteButtons : MonoBehaviour
{
    [SerializeField] private Toggle muteMusicToggle;
    [SerializeField] private Toggle muteSfxToggle;

    private bool isMusicMuted = false;
    private bool isSfxMuted = false;

    private void Start()
    {
        isMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        isSfxMuted = PlayerPrefs.GetInt("SfxMuted", 0) == 1;

        muteMusicToggle.isOn = isMusicMuted;
        muteSfxToggle.isOn = isSfxMuted;

        muteMusicToggle.onValueChanged.AddListener(OnMusicMuteChanged);
        muteSfxToggle.onValueChanged.AddListener(OnSfxMuteChanged);

    }

    private void OnMusicMuteChanged(bool muted)
    {
        isMusicMuted = muted;
        AudioManager.Instance?.audioMixer.SetFloat("MusicVolume", muted ? -80f : 0f);
        PlayerPrefs.SetInt("MusicMuted", muted ? 1 : 0);
    }

    private void OnSfxMuteChanged(bool muted)
    {
        isSfxMuted = muted;
        AudioManager.Instance?.audioMixer.SetFloat("SFXVolume", muted ? -80f : 0f);
        PlayerPrefs.SetInt("SfxMuted", muted ? 1 : 0);
    }
    
    private void ApplyMuteStates()
    {
        AudioManager.Instance?.audioMixer.SetFloat("MusicVolume", isMusicMuted ? -80f : 0f);
        AudioManager.Instance?.audioMixer.SetFloat("SFXVolume", isSfxMuted ? -80f : 0f);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KeySoundTrigger : MonoBehaviour
{
    public string sfxKey = "laser_shoot";
    public KeyCode triggerKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        AudioManager.Instance?.PlaySFX(sfxKey);
    }
}


[Serializable]
public class NamedClip
{
    public string key;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = false;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer + Groups")]
    public AudioMixer audioMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Clips")]
    public List<NamedClip> musicClips = new();
    public List<NamedClip> sfxClips = new();

    private Dictionary<string, NamedClip> _musicMap;
    private Dictionary<string, NamedClip> _sfxMap;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _musicMap = new Dictionary<string, NamedClip>(StringComparer.OrdinalIgnoreCase);
        foreach (var nc in musicClips)
            if (nc?.clip) _musicMap[nc.key] = nc;

        _sfxMap = new Dictionary<string, NamedClip>(StringComparer.OrdinalIgnoreCase);
        foreach (var nc in sfxClips)
            if (nc?.clip) _sfxMap[nc.key] = nc;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.outputAudioMixerGroup = musicGroup;
        _musicSource.playOnAwake = false;
        _musicSource.loop = true;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.outputAudioMixerGroup = sfxGroup;
        _sfxSource.playOnAwake = false;
        _sfxSource.loop = false;

    }

    public void PlayMusic(string key, bool restartIfSame = false)
    {
        if (!_musicMap.TryGetValue(key, out var nc) || nc.clip == null) return;
        if (!restartIfSame && _musicSource.clip == nc.clip && _musicSource.isPlaying) return;

        _musicSource.clip = nc.clip;
        _musicSource.volume = nc.volume;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip = null;
    }

    public void PlaySFX(string key)
    {
        if (_sfxMap.TryGetValue(key, out var nc) && nc.clip != null)
        {
            _sfxSource.PlayOneShot(nc.clip, nc.volume);
        }
    }

    public void SetMasterVolumeDb(float db) => audioMixer?.SetFloat("MasterVolume", db);
    public void SetMusicVolumeDb(float db) => audioMixer?.SetFloat("MusicVolume", db);
    public void SetSFXVolumeDb(float db) => audioMixer?.SetFloat("SFXVolume", db);

    public static float LinearToDb(float linear)
    {
        if (linear <= 0.0001f) return -80f;
        return Mathf.Log10(linear) * 20f;
    }

    public void SetMasterVolume01(float v) => SetMasterVolumeDb(LinearToDb(v));
    public void SetMusicVolume01(float v) => SetMusicVolumeDb(LinearToDb(v));
    public void SetSFXVolume01(float v) => SetSFXVolumeDb(LinearToDb(v));



}

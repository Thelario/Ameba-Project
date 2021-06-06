using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : Singleton<Settings>
{
    public float sfxVolume;
    public float musicVolume;

    private void Start()
    {
        sfxVolume = 1f;
        musicVolume = 1f;
    }

    /* BACKGROUND VOLUME */
    public void SetBackgroundVolumen(float volume)
    {
        musicVolume = volume;
        GameManager.GetInstance().musicAudioSource.volume = musicVolume;
    }

    /* SFX VOLUME */
    public void SetSFXVolumen(float volume)
    {
        sfxVolume = volume;
    }
}

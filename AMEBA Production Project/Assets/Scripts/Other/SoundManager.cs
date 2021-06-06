using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;
using CodeMonkey.Utils;

public static class SoundManager
{
    public enum Sound
    {
        DefenseKillEnemy,
        EnemyKillDefense,
        NeutralFight,
        HitHeartInfection,
        MouseOverButton,
        ButtonClick,
    }

    public enum Music
    {
        MenuMusic,
        InGameMusic,
    }

    // Sound Timer is a dictionary used for having some sounds be played with a timer, which means that that sound will not be called each frame.
    private static Dictionary<Sound, float> soundTimerDictionary;

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static GameObject musicGameObject;
    public static AudioSource musicAudioSource;

    public static void Iinitialize()
    {
        //soundTimerDictionary = new Dictionary<Sound, float>();
        // soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(Sound sound, Vector3 pos, float volume)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = pos;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.volume = volume;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound, Vector3 pos)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = pos;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound)); 
        }
    }

    public static void PlayMusic(Music music)
    {
        // The next method doesn't work for music, because music needs to be looped. It only works for sfx
        //if (musicGameObject == null)
        //{
        //    musicGameObject = new GameObject("Music");
        //    musicAudioSource = musicGameObject.AddComponent<AudioSource>();
        //}

        //musicAudioSource.loop = true;
        //musicAudioSource.Stop();
        //musicAudioSource.PlayOneShot(GetMusicClip(music), Settings.GetInstance().musicVolume);

        GameManager gm = GameManager.GetInstance();
        gm.musicAudioSource.Stop();
        gm.musicAudioSource.clip = GetMusicClip(music);
        gm.musicAudioSource.Play();
    }

    // By default, every sound will be played every time they are called. But with certain sound like the movement
    // of the player, we don't want to play the sound each frame, so we check the timer in the dictionary. Every 
    // sound that needs to check a timer needs to have a case to handle the timer.
    private static bool CanPlaySound(Sound sound)
    {
        // In AMEBA I don't have the necessity to use this method, because sounds will only be played once, and not every frame.
        /*
        switch(sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .3f;

                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                //break;
        }
        */
        return true;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (Assets.SoundAudioClip soundAudioClip in Assets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("SOUND " + sound + " NOT FOUND!");
        return null;
    }

    private static AudioClip GetMusicClip(Music music)
    {
        foreach (Assets.MusicAudioClip musicAudioClip in Assets.i.musicAudioClipArray)
        {
            if (musicAudioClip.sound == music)
            {
                return musicAudioClip.audioClip;
            }
        }
        Debug.LogError("MUSIC " + music + " NOT FOUND!");
        return null;
    }
}

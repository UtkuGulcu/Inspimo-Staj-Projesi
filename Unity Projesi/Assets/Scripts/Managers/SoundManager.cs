using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudioSource;
    
    private const string MUSIC_PREFS = "music";
    private const string SOUND_PREFS = "sound";
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(musicAudioSource);
        }
        else
        {
            Debug.LogError("There are multiple Sound Managers!!");
            Destroy(this);
        }
    }

    private void Start()
    {
        float soundValue = PlayerPrefs.GetFloat(SOUND_PREFS, 0.5f);
        float musicValue = PlayerPrefs.GetFloat(MUSIC_PREFS, 0.5f);

        AudioListener.volume = soundValue;
        musicAudioSource.volume = musicValue;
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        int randomIndex = Random.Range(0, audioClipArray.Length);
        PlaySound(audioClipArray[randomIndex], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(SOUND_PREFS, value);
    }

    public void ChangeMusicVolume(float value)
    {
        musicAudioSource.volume = value;
        PlayerPrefs.SetFloat(MUSIC_PREFS, value);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(SOUND_PREFS, 0.5f);
    }
    
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_PREFS, 0.5f);
    }
}

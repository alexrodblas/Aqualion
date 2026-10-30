using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    public static AudioController instance { get; private set; }

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;

    public float masterVolume = 1.0f;
    public float musicVolume = .5f;
    public float sfxVolume = .5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

    }

    private void Start()
    {
        LoadVolumesPrefs();
    }

    private void LoadVolumesPrefs()
    {
        Master.setVolume(PlayerPrefs.GetFloat("MasterVolume", masterVolume));
        Music.setVolume(PlayerPrefs.GetFloat("MusicVolume", musicVolume));
        SFX.setVolume(PlayerPrefs.GetFloat("SfxVolume", sfxVolume));
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        Master.setVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Music.setVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
        SFX.setVolume(volume);
    }

    public void playOneShoot(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }

    public void playInstance(EventInstance eventInstance)
    {
        eventInstance.start();
    }

    public void stopInstance(EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
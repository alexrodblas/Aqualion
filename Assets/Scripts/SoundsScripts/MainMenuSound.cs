using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    [SerializeField] EventReference musicaFondoMainMenu;
    [SerializeField] EventReference botonPulsado;
    EventInstance musicaFondoMainMenuInstance;
    private void Start()
    {
        musicaFondoMainMenuInstance = RuntimeManager.CreateInstance(musicaFondoMainMenu);

        AudioController.instance.playInstance(musicaFondoMainMenuInstance);
    }

    public void stopMusic()
    {
        AudioController.instance.stopInstance(musicaFondoMainMenuInstance);
    }

    public void SoundOnButtonClick()
    {
        AudioController.instance.playOneShoot(botonPulsado);
    }
}

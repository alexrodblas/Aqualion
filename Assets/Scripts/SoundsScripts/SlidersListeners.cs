using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersListeners : MonoBehaviour
{
    Slider masterSlider;
    Slider musicSlider;
    Slider sfxSlider;

    private void Awake()
    {
        masterSlider = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        musicSlider = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
        sfxSlider = transform.GetChild(2).GetChild(1).GetComponent<Slider>();
    }

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(AudioController.instance.SetMusicVolume);
        masterSlider.onValueChanged.AddListener(AudioController.instance.SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(AudioController.instance.SetSfxVolume);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", AudioController.instance.masterVolume);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", AudioController.instance.musicVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", AudioController.instance.sfxVolume);
    }
}

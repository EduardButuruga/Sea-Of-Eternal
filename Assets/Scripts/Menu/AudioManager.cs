using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public List<AudioSource> sfxSources;

    [Header("UI Elements")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = musicSource.volume;
        sfxSlider.value = 50;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetAllSFXVolumes(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void SetAllSFXVolumes(float volume)
    {
        foreach (AudioSource sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}

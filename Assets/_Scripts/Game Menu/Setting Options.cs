using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private AudioSource musicSource, sfxSource;

    public static SettingOptions instance;
    private Resolution[] resolutions;
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
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey(MenuName.SliderNameMusic))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
            SetMusicVolume();
        }
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int resolutionCurrent = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
                resolutionCurrent = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionCurrent;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel (qualityIndex);
    }
    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat(MenuName.SliderNameMusic, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MenuName.SliderNameMusic, volume);
        musicSource.playOnAwake = true;
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat(MenuName.SliderNameSFX, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MenuName.SliderNameSFX, volume);
    }
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MenuName.SliderNameMusic);
        sfxSlider.value = PlayerPrefs.GetFloat (MenuName.SliderNameSFX);
        SetMusicVolume();
        SetSFXVolume();
    }
}

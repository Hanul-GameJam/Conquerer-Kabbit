using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle audioToggle;

    [SerializeField] float previousVolume;

    public Toggle fullscreenToggle;

    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    void Start()
    {
        audioSource = GameObject.Find("SceneAudio").GetComponent<AudioSource>();

        previousVolume = PlayerPrefs.GetFloat("Volume", 1f);
        audioMixer.SetFloat("MasterVolume", previousVolume);
        volumeSlider.value = previousVolume;

        ApplyAudioState(audioToggle.isOn);

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        audioToggle.onValueChanged.AddListener(OnAudioToggleChange);

        fullscreenToggle.isOn = Screen.fullScreen;

        InitializeResolutionOptions();

        UIManager.hasStarted = true;
    }

    private void OnVolumeChange(float volume)
    {
        if (audioToggle.isOn)
        {
            audioMixer.SetFloat("MasterVolume", volume);
            PlayerPrefs.SetFloat("Volume", volume);
            previousVolume = volume;
        }
    }

    private void OnAudioToggleChange(bool isOn)
    {
        ApplyAudioState(isOn);
    }

    private void ApplyAudioState(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("MasterVolume", previousVolume);
            audioSource.mute = false;
        }
        else
        {
            audioMixer.GetFloat("MasterVolume", out previousVolume);
            audioMixer.SetFloat("MasterVolume", -80f);
            audioSource.mute = false;
        }
    }

    private void InitializeResolutionOptions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ApplyResolution()
    {
        int index = resolutionDropdown.value;
        Resolution resolution = resolutions[index];

        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
    }
}

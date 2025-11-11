using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds1 = new(1f);

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

        bool isAudioOn = PlayerPrefs.GetInt("AudioOn", 1) == 1;
        audioToggle.isOn = isAudioOn;
        ApplyAudioState(isAudioOn);

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;

        InitializeResolutionOptions();
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Resolution resolution = resolutions[savedResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        audioToggle.onValueChanged.AddListener(OnAudioToggleChange);
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);

        StartCoroutine(CheckFullscreen());

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
        PlayerPrefs.SetInt("AudioOn", isOn ? 1 : 0);
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
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullscreen);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void ApplyResolution()
    {
        int index = resolutionDropdown.value;
        Resolution resolution = resolutions[index];

        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    private IEnumerator CheckFullscreen()
    {
        yield return _waitForSeconds1;

        fullscreenToggle.isOn = Screen.fullScreen;
    }
}

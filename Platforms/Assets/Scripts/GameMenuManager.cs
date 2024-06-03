using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices; // Add this line

public class GameMenuManager : MonoBehaviour
{
    [Header("Other Variables")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private PlayerCam cam = null;

    [Header("Volume")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 100.0f; // Changed to 100

    [Header("Graphics")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 100.0f; // Changed to 100

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown ResolutionDropdown;
    private Resolution[] resolutions;

    // Start is called before the first frame update
    private void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        GetPrefs(); // Added call to GetPrefs at the end of Start()
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            ToggleCamera(true);
            SetTimeScale(0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Add this line to load the scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value / 100f; // Convert to 0 to 1 range
        volumeTextValue.text = Mathf.RoundToInt(volumeSlider.value).ToString(); // Show 0 to 100 range
    }

    public void SetBrightness()
    {
        _brightnessLevel = brightnessSlider.value / 100f; // Convert to 0 to 1 range
        brightnessTextValue.text = Mathf.RoundToInt(brightnessSlider.value).ToString(); // Show 0 to 100 range
    }

    public void SetFullScreen()
    {
        _isFullScreen = fullScreenToggle.isOn;
    }

    public void SetQuality()
    {
        _qualityLevel = qualityDropdown.value;
    }

    public void SetResolution()
    {
        int resolutionIndex = ResolutionDropdown.value;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            Debug.LogError("Resolution index is out of bounds.");
        }
    }

    public void Apply()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume * 100f); // Save as 0 to 100
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel * 100f); // Save as 0 to 100

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        PlayerPrefs.SetInt("masterResolution", ResolutionDropdown.value); // Save resolution index
    }

    public void ResetGraphics()
    {
        brightnessSlider.value = defaultBrightness;
        brightnessTextValue.text = Mathf.RoundToInt(defaultBrightness).ToString(); // Show 0 to 100

        qualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1);

        fullScreenToggle.isOn = false;
        Screen.fullScreen = false;

        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
        ResolutionDropdown.value = GetCurrentResolutionIndex(); // Set to the current resolution index

        Apply();
    }

    public void ResetAudio()
    {
        AudioListener.volume = defaultVolume / 100f; // Convert to 0 to 1 range
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = Mathf.RoundToInt(defaultVolume).ToString(); // Show 0 to 100

        Apply();
    }

    private void GetPrefs()
    {
        if (PlayerPrefs.HasKey("CanUse"))
        {
            canUse = true;
        }

        if (canUse)
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                float localVolume = PlayerPrefs.GetFloat("Volume");

                volumeTextValue.text = Mathf.RoundToInt(localVolume).ToString(); // Show 0 to 100
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume / 100f; // Convert to 0 to 1 range
            }

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");

                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("masterFullscreen"))
            {
                int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

                if (localFullscreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = Mathf.RoundToInt(localBrightness).ToString(); // Show 0 to 100
                brightnessSlider.value = localBrightness;
                _brightnessLevel = localBrightness / 100f; // Convert to 0 to 1 range
            }

            if (PlayerPrefs.HasKey("masterResolution"))
            {
                int resolutionIndex = PlayerPrefs.GetInt("masterResolution");
                if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
                {
                    ResolutionDropdown.value = resolutionIndex;
                    Resolution resolution = resolutions[resolutionIndex];
                    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
                }
            }
        }

        PlayerPrefs.SetInt("CanUse", 1);
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                return i;
            }
        }
        return 0; // Default to first resolution if not found
    }

    public void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void ToggleCamera(bool toggle)
    {
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        cam.paused = toggle;
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class ResolutionManager : MonoBehaviour
{
    [Tooltip("Dropdown containing screen resolutions")]
    public TMP_Dropdown resolutionDropdown;
    [Tooltip("Dropdown containing anti-aliasing settings")]
    public TMP_Dropdown aaDropdown;
    [Tooltip("Dropdown containing quality settings for shadows settings")]
    public TMP_Dropdown shadowsDropdown;
    [Tooltip("Toggle that will turn on or off fullscreen mode")]
    public Toggle fullscreenToggle;
    [Tooltip("Toggle that will turn on or off HDR")]
    public Toggle hdrToggle;
    [Tooltip("Level of details slider")]
    public Slider shadowsSlider;
    [Tooltip("Load prompt for saving current settings")]
    public GameObject confirmSettings;
    [Tooltip("Buttons to disable when ConfirmSettings screen is shown")]
    public Button[] buttons;
    [Tooltip("Sliders to disable")]
    public Slider[] sliders;

    public bool SettingsChanged
    {
        set => _settingsChanged = value;
    }
    
    private int _fullscreen;
    private bool _settingsChanged = false;

    private Resolution[] _resolutions;
    private HDROutputSettings _hdrSettings;
    private FPSTarget _fpsTarget;
    private GameObject _tempObject;
    
    public void SetResolution()
    {
        Resolution resolution = _resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, GetFullscreen());
        _settingsChanged = true;
        Debug.Log("Resolution set to: " + resolution.width + "x" + resolution.height);
    }

    public void SetAntiAliasing()
    {
        QualitySettings.antiAliasing = aaDropdown.value switch
        {
            0 => 0,
            1 => 2,
            2 => 4,
            3 => 8,
            _ => QualitySettings.antiAliasing
        };

        _settingsChanged = true;
        Debug.Log("AA set to: " + QualitySettings.antiAliasing);
    }

    public void SetShadows()
    {
        QualitySettings.shadows = shadowsDropdown.value switch
        {
            0 => ShadowQuality.Disable,
            1 => ShadowQuality.HardOnly,
            2 => ShadowQuality.All,
            _ => ShadowQuality.Disable
        };
        
        _settingsChanged = true;
        Debug.Log("Shadows set to: " + QualitySettings.shadows);
    }

    public void SetShadowsResolution()
    {
        QualitySettings.shadowResolution = shadowsSlider.value switch
        {
            0 => ShadowResolution.Low,
            1 => ShadowResolution.Medium,
            2 => ShadowResolution.High,
            3 => ShadowResolution.VeryHigh,
            _ => ShadowResolution.Low
        };
        
        _settingsChanged = true;
        Debug.Log("Shadows resolution set to: " + QualitySettings.shadowResolution);
    }

    public void SetHDR()
    {
        if (!_hdrSettings.available) return;
        _hdrSettings.RequestHDRModeChange(hdrToggle.isOn);

        _settingsChanged = true;
        Debug.Log("HDR set to: " + _hdrSettings.active);
    }

    public void SetFullscreen()
    {
        _fullscreen = fullscreenToggle.isOn ? 1 : 0;
        _settingsChanged = true;
        Debug.Log("Fullscreen: " + fullscreenToggle.isOn.ToString());
    }

    private bool GetFullscreen()
    {
        return _fullscreen == 1;
    }

    public void ApplySettings()
    {
        SetFullscreen();
        SetResolution();
        SetAntiAliasing();
        SetShadows();
        SetShadowsResolution();
        SetHDR();
        _fpsTarget.Restore();
    }

    public void ShowConfirmScreen()
    {
        confirmSettings = Instantiate(confirmSettings, GameObject.Find("Canvas").transform, true);
        confirmSettings.transform.localScale = new Vector3(1, 1, 1);
        confirmSettings.transform.localPosition = new Vector3(1, 1, 1);
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstRun", 0);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", _fullscreen);
        PlayerPrefs.SetInt("AASettings", aaDropdown.value);
        PlayerPrefs.SetInt("ShadowsSettings", shadowsDropdown.value);
        PlayerPrefs.SetInt("ShadowsResolution", Convert.ToInt32(shadowsSlider.value));
        PlayerPrefs.SetInt("IngameFPS", _fpsTarget.GameFPS);
        PlayerPrefs.SetInt("MenuFPS", _fpsTarget.MenuFPS);
        
        if (_hdrSettings.available)
        {
            PlayerPrefs.SetString("HDRSettings", _hdrSettings.active.ToString());
        }
        
        
        PlayerPrefs.Save();
        Debug.Log("Resolution settings saved");
    }

    public void RestoreSettings()  
    {
        _fullscreen = PlayerPrefs.GetInt("Fullscreen", 0);
        if (_fullscreen == 1)
        { 
            fullscreenToggle.isOn = true;
        }
        else 
        {
            _fullscreen = 0; 
            fullscreenToggle.isOn = false;
        }
        Debug.Log("Fullscreen settings restored");
        
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", resolutionDropdown.value);
        Debug.Log("Resolution settings restored");

        aaDropdown.value = PlayerPrefs.GetInt("AASettings", 0);
        Debug.Log("AA settings restored");

        shadowsDropdown.value = PlayerPrefs.GetInt("ShadowsSettings", 0);
        Debug.Log("Shadows settings restored");

        shadowsSlider.value = PlayerPrefs.GetInt("ShadowsResolution", 0);
        Debug.Log("Shadows resolution restored");

        if (_hdrSettings.available)
        {
            hdrToggle.enabled = true;
            
            hdrToggle.isOn =
                Convert.ToBoolean(PlayerPrefs.GetString("HDRSettings", "false"));
                Debug.Log("HDR settings restored");
        }
        else
        {
            hdrToggle.enabled = false;
            hdrToggle.interactable = false;
            Debug.Log("HDR settings not available");
        }

        _fpsTarget.GameFPS = PlayerPrefs.GetInt("IngameFPS", 60);
        Debug.Log("In-game FPS restored.");


        _fpsTarget.MenuFPS = PlayerPrefs.GetInt("MenuFPS", 30);
        Debug.Log("Menu FPS restored.");

        ApplySettings();
        _settingsChanged = false;
    }

    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.enabled = false;
        }

        foreach (Slider slider in sliders)
        {
            slider.enabled = false;
        }

        aaDropdown.enabled = false;
        resolutionDropdown.enabled = false;
        shadowsDropdown.enabled = false;
        shadowsSlider.enabled = false;
        fullscreenToggle.enabled = false;
        
        if (_hdrSettings.available)
        {
            hdrToggle.enabled = false;
        }
    }
    
    public void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.enabled = true;
        }
        
        foreach (Slider slider in sliders)
        {
            slider.enabled = true;
        }
        
        aaDropdown.enabled = true;
        resolutionDropdown.enabled = true;
        shadowsDropdown.enabled = true;
        shadowsSlider.enabled = true;
        fullscreenToggle.enabled = true;
        if (_hdrSettings.available)
        {
            hdrToggle.enabled = true;
        }
    }

    public void Destroy()
    {
        if (confirmSettings == null) 
            return;
        
        EnableButtons();
        Destroy(confirmSettings);
        confirmSettings = _tempObject;
    }

    public void GetFPSTarget(FPSTarget target)
    {
        _fpsTarget = target;
    }

    private List<string> GenerateResolutionOptions()
    {
        List<string> dropdownOptions = new List<string>();
        foreach (Resolution resolution in _resolutions)
        {
            dropdownOptions.Add(resolution.ToString()); 
        }
        return dropdownOptions;
    }

    private void FirstRun(List<string> resolutionOptions)
    {
        if (_hdrSettings.available)
        {
            hdrToggle.enabled = true;
            hdrToggle.interactable = true;
            hdrToggle.isOn = false;
        }
        else
        {
            hdrToggle.enabled = false;
            hdrToggle.interactable = false;
            hdrToggle.isOn = false;
        }

        aaDropdown.value = 0;
        shadowsDropdown.value = 0;
        shadowsSlider.value = 0;
        resolutionDropdown.value = FindScreenResolution(resolutionOptions);
        fullscreenToggle.isOn = false;
        _fpsTarget.gameFPSSlider.value = 60;
        _fpsTarget.GameFPS = 60;
        _fpsTarget.menuFPSSlider.value = 30;
        _fpsTarget.MenuFPS = 30;
        
        ApplySettings();
        SaveSettings();
        
        _settingsChanged = false;
    }

    private int FindScreenResolution(List<string> resolutionOptions)
    {
        string desktopResolution = Screen.currentResolution.ToString();
        int index = 0;

        foreach (string resolution in resolutionOptions)
        {
            if (resolution == desktopResolution)
            {
                return index;
            }

            index++;
        }

        return index;
    }

    private void Start()
    {
        _tempObject = confirmSettings;
        _resolutions = Screen.resolutions;
        _hdrSettings = HDROutputSettings.main;

        List<string> resolutionOptions = GenerateResolutionOptions();

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);
        
        if (!PlayerPrefs.HasKey("FirstRun") || PlayerPrefs.GetInt("FirstRun", 1) == 1)
        {
            FirstRun(resolutionOptions);
        }
        else
        {
            RestoreSettings();
        }
    }

    private void Update()
    {
        if (_settingsChanged)
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
        }
        else
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
        }
    }
}

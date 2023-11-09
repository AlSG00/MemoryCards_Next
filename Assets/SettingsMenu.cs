using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private List<ScreenResolution> _resolutionCollection = new List<ScreenResolution>();
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private TextMeshProUGUI _currentResolutionText;
    [SerializeField] private SavedSettings _savedSettings;
    private int _currentResolution;

    private void Start()
    {
        //Screen.SetResolution(
        //    PlayerPrefs.GetInt("resHoriz", 1280),
        //    PlayerPrefs.GetInt("resVert", 720),
        //    FullScreenMode.FullScreenWindow,
        //    144
        //    );
        _currentResolution = PlayerPrefs.GetInt("res", 0);
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume", 1.0f);
        _masterVolumeSlider.value = AudioListener.volume;
        //AudioListener.volume = _savedSettings.masterVolume;

        //_currentResolution = _savedSettings.currentResolutionPreset;

        ChangeResolution();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _masterVolumeSlider.value;
        //_savedSettings.masterVolume = AudioListener.volume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void ChangeResolution()
    {
        int choosenWidth = _resolutionCollection[_currentResolution].horizontalValue;
        int choosenHeight = _resolutionCollection[_currentResolution].verticalValue;
        _currentResolutionText.text = $"{choosenWidth}x{choosenHeight}";
        Screen.SetResolution(choosenWidth, choosenHeight, FullScreenMode.FullScreenWindow, 144);
        _savedSettings.currentResolutionPreset = _currentResolution;
        //PlayerPrefs.SetInt("resHoriz", choosenWidth);
        //PlayerPrefs.SetInt("resVert", choosenHeight);
        //PlayerPrefs.Save();
    }

    public void IncreaseResolution()
    {
        _currentResolution++;
        if (_currentResolution == _resolutionCollection.Count)
        {
            _currentResolution = 0;
        }
        PlayerPrefs.SetInt("res", _currentResolution);
        ChangeResolution();
    }

    public void DecreaseResolution()
    {
        _currentResolution--;
        if (_currentResolution == -1)
        {
            _currentResolution = _resolutionCollection.Count - 1;
        }
        PlayerPrefs.SetInt("res", _currentResolution);
        ChangeResolution();
    }

    [System.Serializable]
    public class ScreenResolution
    {
        public int horizontalValue;
        public int verticalValue;
    }
}

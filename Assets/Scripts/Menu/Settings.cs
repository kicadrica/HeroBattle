using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle SoundToggle;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] private Slider MusicSlider;
    
    public static event Action OnSoundVolumeChange;
    public static float SoundsVolume {
        get => 1 - PlayerPrefs.GetFloat("SoundVolume");
        private set => PlayerPrefs.SetFloat("SoundVolume", 1 - value);
    }
    
    public static event Action OnMusicVolumeChange;
    public static float MusicVolume {
        get => 1 - PlayerPrefs.GetFloat("MusicVolume");
        private set => PlayerPrefs.SetFloat("MusicVolume", 1 - value);
    }
    
    private bool _ignoreEvents;
    
    private void Awake()
    {
        SoundSlider.value = SoundsVolume;
        MusicSlider.value = MusicVolume;
        
        SoundToggle.isOn = SoundsVolume > 0;
        MusicToggle.isOn = MusicVolume > 0;
        
        SoundSlider.onValueChanged.AddListener(ControlSoundsVolume);
        MusicSlider.onValueChanged.AddListener(ControlMusicVolume);
        
        SoundToggle.onValueChanged.AddListener(SwitchToggleSounds);
        MusicToggle.onValueChanged.AddListener(SwitchToggleMusic);
    }

   
    
    private void SwitchToggleSounds(bool isOn)
    {
        if (_ignoreEvents) return;
        if(!isOn) {
            SoundSlider.value = 0;
        }
        else
        {
            SoundSlider.value = 1;
        }
    }

    private void ControlSoundsVolume(float soundValue)
    {
        SoundsVolume = soundValue;
        
        _ignoreEvents = true;
        SoundToggle.isOn = SoundsVolume > 0;
        _ignoreEvents = false;
        
        OnSoundVolumeChange?.Invoke();
    }
    
    private void SwitchToggleMusic(bool isOn)
    {
        if (_ignoreEvents) return;
        if(!isOn) {
            MusicSlider.value = 0;
        }
        else
        {
            MusicSlider.value = 1;
        }
    }
    
    private void ControlMusicVolume(float musicValue)
    {
        MusicVolume = musicValue;
        
        _ignoreEvents = true;
        MusicToggle.isOn = MusicVolume > 0;
        _ignoreEvents = false;
        
        OnMusicVolumeChange?.Invoke();
    }
}

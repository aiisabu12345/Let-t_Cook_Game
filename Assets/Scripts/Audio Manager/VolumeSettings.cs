using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider musicSilder;
    [SerializeField] private Slider SFXSilder;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
           SetSFXVolume();
        }
        
    }
    public void SetMusicVolume()
    {
        float volume = musicSilder.value;
        MyMixer.SetFloat("music", Mathf.Log10(volume)*20);
        //set player pref
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSilder.value;
        MyMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        //set player pref
        PlayerPrefs.SetFloat("SFXVolume", volume);
    } 
    private void LoadVolume()
    {
        musicSilder.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSilder.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
       SetSFXVolume();
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private const float minVolume = -80f; // Unity's mute level

    private void Start()
    {
        LoadVolume();
    }

    // Set the master volume based on the slider's value
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        float dB = volume > 0 ? Mathf.Log10(volume) * 20 : minVolume;
        audioMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    // Set the background music volume based on the slider's value
    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        float dB = volume > 0 ? Mathf.Log10(volume) * 20 : minVolume;
        audioMixer.SetFloat("BGMVolume", dB);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    // Set the sound effects volume based on the slider's value
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        float dB = volume > 0 ? Mathf.Log10(volume) * 20 : minVolume;
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // Load the saved volume values from PlayerPrefs
    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
            masterSlider.value = savedMasterVolume;
            SetMasterVolume();
            Debug.Log("Master Volume Loaded: " + savedMasterVolume);
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume");
            bgmSlider.value = savedBGMVolume;
            SetBGMVolume();
            Debug.Log("BGM Volume Loaded: " + savedBGMVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = savedSFXVolume;
            SetSFXVolume();
            Debug.Log("SFX Volume Loaded: " + savedSFXVolume);
        }
    }

    // Ensure volume is saved when the application quits
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}

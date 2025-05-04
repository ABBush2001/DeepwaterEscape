using UnityEngine;
using UnityEngine.Audio;

public class VolumeLoader : MonoBehaviour
{
    [SerializeField][Tooltip("The master audio mixer.")] private AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        masterMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        masterMixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
        masterMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }
}

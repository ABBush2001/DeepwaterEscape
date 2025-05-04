using UnityEngine;
using UnityEngine.Audio;

public class VolumeLoader : MonoBehaviour
{
    [SerializeField][Tooltip("The master audio mixer.")] private AudioMixer masterMixer;

    private const float minVol = -80f; // Unity's mute level

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve values
        float masterVol = PlayerPrefs.GetFloat("MasterVolume");
        float BGMVol = PlayerPrefs.GetFloat("BGMVolume");
        float SFXVol = PlayerPrefs.GetFloat("SFXVolume");

        // If values are above 0, convert them to suitable audio mixer values.
        masterVol = PlayerPrefs.GetFloat("MasterVolume") > 0 ? Mathf.Log10(masterVol) * 20 : minVol;
        BGMVol = PlayerPrefs.GetFloat("BGMVolume") > 0 ? Mathf.Log10(BGMVol) * 20 : minVol;
        SFXVol = PlayerPrefs.GetFloat("SFXVolume") > 0 ? Mathf.Log10(SFXVol) * 20 : minVol;

        // Set the mixer volume.
        masterMixer.SetFloat("MasterVolume", masterVol);
        masterMixer.SetFloat("BGMVolume", BGMVol);
        masterMixer.SetFloat("SFXVolume", SFXVol);
    }
}
